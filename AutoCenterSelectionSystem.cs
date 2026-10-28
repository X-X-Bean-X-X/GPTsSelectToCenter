using Game;
using Game.Rendering;
using Game.UI.InGame;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Scripting;

namespace SelectToCenter
{
    public partial class AutoCenterSelectionSystem : GameSystemBase
    {
        private const float kCenterTolerance = 0.035f;

        private SelectedInfoUISystem m_SelectedInfoSystem;
        private CameraUpdateSystem m_CameraUpdateSystem;

        private Entity m_CurrentFollowedEntity;
        private double m_StopFollowAt = -1d;
        private bool m_IsSubscribed;

        [Preserve]
        public AutoCenterSelectionSystem()
        {
        }

        protected override void OnCreate()
        {
            base.OnCreate();

            m_SelectedInfoSystem = World.GetOrCreateSystemManaged<SelectedInfoUISystem>();
            m_CameraUpdateSystem = World.GetOrCreateSystemManaged<CameraUpdateSystem>();
        }

        protected override void OnStartRunning()
        {
            base.OnStartRunning();
            Subscribe();
        }

        protected override void OnStopRunning()
        {
            Unsubscribe();
            StopFollowingNow();
            base.OnStopRunning();
        }

        protected override void OnUpdate()
        {
            if (Mod.ConsumeClearSelectionRequest())
            {
                StopFollowingNow();

                if (m_SelectedInfoSystem != null)
                {
                    m_SelectedInfoSystem.SetSelection(Entity.Null);
                }
            }

            if (Mod.Settings == null || !Mod.Settings.Enabled)
            {
                StopFollowingNow();
                Mod.ClearNextSelectionArm();
                return;
            }

            if (m_CurrentFollowedEntity == Entity.Null)
                return;

            if (!EntityManager.Exists(m_CurrentFollowedEntity))
            {
                StopFollowingNow();
                return;
            }

            if (!Mod.Settings.KeepFocusOnSelect &&
                (HasReachedCenter() || (m_StopFollowAt >= 0d && World.Time.ElapsedTime >= m_StopFollowAt)))
            {
                StopFollowingNow();
            }
        }

        private void Subscribe()
        {
            if (m_IsSubscribed || m_SelectedInfoSystem == null)
                return;

            m_SelectedInfoSystem.eventSelectionChanged += OnSelectionChanged;
            m_IsSubscribed = true;
        }

        private void Unsubscribe()
        {
            if (!m_IsSubscribed || m_SelectedInfoSystem == null)
                return;

            m_SelectedInfoSystem.eventSelectionChanged -= OnSelectionChanged;
            m_IsSubscribed = false;
        }

        private void OnSelectionChanged(Entity entity, Entity prefab, float3 position)
        {
            if (Mod.Settings == null || !Mod.Settings.Enabled)
            {
                StopFollowingNow();
                return;
            }

            if (entity == Entity.Null)
            {
                StopFollowingNow();
                return;
            }

            if (Mod.Settings.RequireArmForNextSelection && !Mod.ConsumeNextSelectionArm())
            {
                StopFollowingNow();
                return;
            }

            if (m_CurrentFollowedEntity != Entity.Null && m_CurrentFollowedEntity != entity)
            {
                StopFollowingNow();
            }

            m_CurrentFollowedEntity = entity;
            m_SelectedInfoSystem.Focus(entity);
            m_StopFollowAt = Mod.Settings.KeepFocusOnSelect
                ? -1d
                : World.Time.ElapsedTime + Setting.kHoldSeconds;
        }

        private bool HasReachedCenter()
        {
            if (m_SelectedInfoSystem == null || m_SelectedInfoSystem.selectedEntity == Entity.Null)
                return false;

            Camera camera = Camera.main;
            if (camera == null)
                return false;

            float3 selectedPosition = m_SelectedInfoSystem.selectedPosition;
            Vector3 viewport = camera.WorldToViewportPoint(
                new Vector3(selectedPosition.x, selectedPosition.y, selectedPosition.z));

            if (viewport.z <= 0f)
                return false;

            return math.abs(viewport.x - 0.5f) <= kCenterTolerance
                && math.abs(viewport.y - 0.5f) <= kCenterTolerance;
        }

        private void StopFollowingNow()
        {
            if (m_CurrentFollowedEntity == Entity.Null && m_StopFollowAt < 0d)
                return;

            if (m_CameraUpdateSystem != null)
            {
                var orbit = m_CameraUpdateSystem.orbitCameraController;
                var gameplay = m_CameraUpdateSystem.gamePlayController;

                if (orbit != null && gameplay != null)
                {
                    gameplay.TryMatchPosition(orbit);
                    m_CameraUpdateSystem.activeCameraController = gameplay;
                    orbit.followedEntity = Entity.Null;
                }
                else if (m_SelectedInfoSystem != null)
                {
                    m_SelectedInfoSystem.Focus(Entity.Null);
                }
            }
            else if (m_SelectedInfoSystem != null)
            {
                m_SelectedInfoSystem.Focus(Entity.Null);
            }

            m_CurrentFollowedEntity = Entity.Null;
            m_StopFollowAt = -1d;
        }
    }
}