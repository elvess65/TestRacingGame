using com.example.map;
using System.Collections.Generic;
using UnityEngine;

namespace com.example.unity.map.creator
{
    public class MapCreator : MonoBehaviour
    {
        public MapLoader LoaderOfMap;

        private IMap m_Map;
        private bool m_IsLevelLoaded = false;
        private int m_LastAddedCheckPointIndex = 0;

        public IMap GetMap()
        {
            return m_Map;
        }

        public bool IsLevelLoaded()
        {
            return m_IsLevelLoaded;
        }

        public void CreateLevel(int width, int height)
        {
            ClearLevel();

            IMapFactory mapFactory = new DefaultMapFactory();
            m_Map = mapFactory.CreateMap(width, height);

            LoaderOfMap.CreateMap(m_Map, true);
            AssignCellParams();

            m_IsLevelLoaded = true;
        }

        public void ClearLevel()
        {
            m_Map = null;
            LoaderOfMap.ClearMap();
            m_IsLevelLoaded = false;
        }

        public void SaveMap(string mapName)
        {
            IMapSerializator serializator = new FileMapSerializator("Assets/Resources/Maps/" + mapName);
            serializator.SaveMap(m_Map);
        }

        public void LoadMap(string mapName)
        {
            if (m_IsLevelLoaded)
                ClearLevel();

            IMapSerializator serializator = new FileMapSerializator("Assets/Resources/Maps/" + mapName);
            m_Map = serializator.LoadMap();

            LoaderOfMap.CreateMap(m_Map, true);
            AssignCellParams();

            m_IsLevelLoaded = true;
        }

        private void AssignCellParams()
        {
            for (int x = 0; x < m_Map.GetWidth(); ++x)
                for (int y = 0; y < m_Map.GetHeight(); ++y)
                {
                    GameObject cellObj = LoaderOfMap.GetCellAt(x, y);
                    IMapCell mapCell = m_Map.GetCell(x, y);

                    AssignCellParam(cellObj, mapCell, x, y);
                }
        }

        private void AssignCellParam(GameObject go, IMapCell cell, int x, int y)
        {
            ClearOtherColliders(go);

            MapCellParam cellParam = go.AddComponent<MapCellParam>();
            BoxCollider col = go.AddComponent<BoxCollider>();
            col.size = new Vector3(2, 1, 2);

            cellParam.CellType = (MapCellTypes)cell.GetCellType();
            cellParam.X = x;
            cellParam.Y = y;
        }

        private void ClearOtherColliders(GameObject go)
        {
            Collider[] childColliders = go.GetComponentsInChildren<Collider>();
            if (childColliders != null)
                foreach (Collider child in childColliders)
                    DestroyImmediate(child);
        }

        public void UpdateMapCellValue(int x, int y, byte type, byte surfaceType)
        {
            IMapCell cell = m_Map.GetCell(x, y);

            cell.SetCellType(type);
            cell.SetSurfaceType(surfaceType);

            LoaderOfMap.DestroyCellAt(x, y);
            GameObject newCellGo = LoaderOfMap.CreateCellAt(x, y, cell.GetCellType(), surfaceType);

            AssignCellParam(newCellGo, cell, x, y);
        }

        public void AddCheckPoint(int x, int y, byte type)
        {
            DefaultCheckPoint checkPoint = new DefaultCheckPoint(x, y, type, m_LastAddedCheckPointIndex++);
            m_Map.GetCheckPointsManager().AddCheckPoint(checkPoint);

            LoaderOfMap.CreateCheckPoint(checkPoint);
        }

        public bool HasCheckPointAt(int x, int y)
        {
            ICollection<ICheckPoint> checkpoints = m_Map.GetCheckPointsManager().GetCheckPoints();
            foreach (ICheckPoint cp in checkpoints)
            {
                if (cp.GetX().Equals(x) && cp.GetY().Equals(y))
                    return true;
            }

            return false;
        }

        public void RemoveCheckPoint(int x, int y)
        {
            ICollection<ICheckPoint> checkpoints = m_Map.GetCheckPointsManager().GetCheckPoints();
            int indexToRemove = -1;

            foreach (ICheckPoint cp in checkpoints)
            {
                if (cp.GetX().Equals(x) && cp.GetY().Equals(y))
                {
                    indexToRemove = cp.GetIndex();
                    break;
                }
            }

            if (indexToRemove != -1)
            {
                m_Map.GetCheckPointsManager().RemoveCheckPoint(indexToRemove);
                LoaderOfMap.RemoveCheckPointAt(x, y);
            }
        }

        void OnDestroy()
        {
            ClearLevel();
        }
    }
}