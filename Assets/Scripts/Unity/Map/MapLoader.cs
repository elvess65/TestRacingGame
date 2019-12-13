using com.example.map;
using System.Collections.Generic;
using UnityEngine;

namespace com.example.unity.map
{
    public class MapLoader : MonoBehaviour
    {
        public int Width = 20;
        public int Height = 20;
        public float CellSize = 3.8f;
        public GameObject[] CellPrefabs;
        public GameObject[] CheckPointPrefabs;

        private GameObject m_Root;
        private GameObject[][] m_Cells;
        private List<UnityCheckPoint> m_CheckPoints = new List<UnityCheckPoint>();

        public void CreateMap(IMap map, bool createEmptyCells)
        {
            m_Root = new GameObject("map_root");
            m_Cells = new GameObject[map.GetWidth()][];

            for (int x = 0; x < map.GetWidth(); ++x)
            {
                m_Cells[x] = new GameObject[map.GetHeight()];

                for (int y = 0; y < map.GetHeight(); ++y)
                {
                    IMapCell mapCell = map.GetCell(x, y);

                    if (!createEmptyCells && mapCell.GetCellType().Equals(0))
                        continue;
 
                    byte cellPrefabIndex = mapCell.GetCellType();
                    byte surfaceType = mapCell.GetSurfaceType();

                    CreateCellAt(x, y, cellPrefabIndex, surfaceType);
                }
            }

            ICheckPointsManager checkPointsManager = map.GetCheckPointsManager();
            foreach (ICheckPoint checkPoint in checkPointsManager.GetCheckPoints())
                CreateCheckPoint(checkPoint);
        }

        public GameObject GetCellAt(int x, int y)
        {
            return m_Cells[x][y];
        }

        public void DestroyCellAt(int x, int y)
        {
            DestroyImmediate(m_Cells[x][y]);
        }

        public GameObject CreateCellAt(int x, int y, byte type, byte surfaceType)
        {
            GameObject cellObj = Instantiate(CellPrefabs[type], m_Root.transform);

            cellObj.name = x + " " + y;
            cellObj.transform.position = new Vector3(x * CellSize, 0, y * CellSize);

            if (surfaceType == 1)
            {
                for (int i = 0; i < cellObj.transform.childCount; ++i)
                    if (cellObj.transform.GetChild(i).name.Equals("Ground"))
                    {
                        GameObject groundObj = cellObj.transform.GetChild(i).gameObject;

                        Renderer rend = groundObj.GetComponent<Renderer>();
                        Material mat = new Material(rend.sharedMaterial);
                        mat.SetTextureOffset("_MainTex", new Vector2(0, 0.5f)); // simulate sand

                        rend.material = mat;

                        break;
                    }
            }

            m_Cells[x][y] = cellObj;

            return cellObj;
        }

        public void ClearMap()
        {
            DestroyImmediate(m_Root);
            m_CheckPoints.Clear();
        }

        public GameObject CreateCheckPoint(ICheckPoint checkPoint)
        {
            GameObject go = Instantiate(CheckPointPrefabs[checkPoint.GetPointType()], m_Root.transform);
            UnityCheckPoint unityCheckPoint = go.GetComponent<UnityCheckPoint>();

            go.name = "checkPoint " + checkPoint.GetX() + " " + checkPoint.GetY();
            go.transform.position = new Vector3(checkPoint.GetX() * CellSize, 0, checkPoint.GetY() * CellSize);

            m_CheckPoints.Add(unityCheckPoint);

            unityCheckPoint.Init(checkPoint);

            return go;
        }
         
        public void RemoveCheckPointAt(int x, int y)
        {
            for (int i = 0; i < m_CheckPoints.Count; ++i)
            {
                ICheckPoint checkPointObj = m_CheckPoints[i].GetCheckPoint();
                if (checkPointObj.GetX().Equals(x) && checkPointObj.GetY().Equals(y))
                {
                    DestroyImmediate(m_CheckPoints[i].gameObject);
                    m_CheckPoints.RemoveAt(i);

                    break;
                }
            }
        }

        public UnityCheckPoint GetCheckPoint(int index)
        {
            return m_CheckPoints[index];
        }

        public ICollection<UnityCheckPoint> GetCheckPoints()
        {
            return m_CheckPoints;
        }

        public byte GetSurfaceTypeAt(IMap map, Vector3 pos)
        {
            int x = Mathf.CeilToInt(pos.x / CellSize);
            int y = Mathf.CeilToInt(pos.z / CellSize);

            return map.GetCell(x, y).GetSurfaceType();
        }
    }
}