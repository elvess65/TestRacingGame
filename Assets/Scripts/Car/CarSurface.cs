
using System.Collections.Generic;

namespace com.example.car
{
    public class CarSurface : ISurfaceInfo
    {
        private Dictionary<byte, SurfaceInfo> m_Surfaces = new Dictionary<byte, SurfaceInfo>();
        private byte m_CurrentSurface;

        public void AddSurface(byte surfaceType, float accelerationOnSurface, float speedMultyplayer)
        {
            if (m_Surfaces.Count <= 0)
                SetCurrentSurface(surfaceType);

            if (!m_Surfaces.ContainsKey(surfaceType))
                m_Surfaces.Add(surfaceType, new SurfaceInfo(accelerationOnSurface, speedMultyplayer));
            else
                m_Surfaces[surfaceType] = new SurfaceInfo(accelerationOnSurface, speedMultyplayer);
        }

        public void SetCurrentSurface(byte sutface)
        {
            m_CurrentSurface = sutface;
        }

        public byte GetCurrentSurface()
        {
            return m_CurrentSurface;
        }

        public float GetSurfaceAcceleration()
        {
            return m_Surfaces[m_CurrentSurface].Acceleration;
        }

        public float GetSurfaceSpeedMultiplayer()
        {
            return m_Surfaces[m_CurrentSurface].SpeedMultyplayer;
        }
    }
}