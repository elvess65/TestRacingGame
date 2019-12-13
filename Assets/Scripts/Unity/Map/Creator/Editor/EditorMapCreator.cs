using UnityEditor;
using UnityEngine;

namespace com.example.unity.map.creator.editor
{
    [CustomEditor(typeof(MapCreator))]
    public class EditorMapCreator : Editor
    {
        private MapCellTypes m_PaintCellType = MapCellTypes.RoadUpDown;
        private CellSurfaceTypes m_CellSurfaceType = CellSurfaceTypes.Grass;
        private CheckPointTypes m_CheckPointType = CheckPointTypes.LeftRight;
        private string m_MapName = "Level_10";
        private int m_Width = 20;
        private int m_Height = 20;
        private bool m_IsPaintCell = false;
        private bool m_IsAddingCheckpoints = false;
        private bool m_IsRemovingCheckpoints = false;
        private bool m_IsEdited = false;

        private void OnEnable()
        {
            MapCreator obj = (MapCreator)target;
            if (obj.GetMap() == null)
                obj.ClearLevel();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            MapCreator obj = (MapCreator)target;

            m_MapName = EditorGUILayout.TextField("Map Name", m_MapName);

            if (obj.IsLevelLoaded())
            {
                Color prev = GUI.color;

                if (!m_IsAddingCheckpoints && !m_IsRemovingCheckpoints)
                    ShowPaintCellButton();

                GUI.color = prev;

                if (!m_IsPaintCell && !m_IsRemovingCheckpoints)
                    ShowAddCheckPointsButton();

                GUI.color = prev;

                if (!m_IsPaintCell && !m_IsAddingCheckpoints)
                    ShowRemoveCheckPointsButton();

                GUI.color = prev;

                EditorGUILayout.Space();

                if (m_IsEdited)
                    GUI.color = Color.green;

                if (GUILayout.Button("Save Level"))
                {
                    obj.SaveMap(m_MapName);
                    m_IsEdited = false;
                }

                if (m_IsEdited)
                    GUI.color = prev;

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                if (GUILayout.Button("Clear Level"))
                {
                    obj.ClearLevel();
                }
            }
            else
            {
                m_Width = EditorGUILayout.IntField("Map Width", m_Width);
                m_Height = EditorGUILayout.IntField("Map Height", m_Height);

                if (GUILayout.Button("Create Level"))
                    obj.CreateLevel(m_Width, m_Height);
                else if (GUILayout.Button("Load Level"))
                    obj.LoadMap(m_MapName);
            }
        }

        private void ShowPaintCellButton()
        {
            if (!m_IsPaintCell)
            {
                GUI.color = Color.green;
                if (GUILayout.Button("Start Paint Cells"))
                    m_IsPaintCell = true;
            }
            else
            {
                m_PaintCellType = (MapCellTypes)EditorGUILayout.EnumPopup("Map Cell Type", m_PaintCellType);
                m_CellSurfaceType = (CellSurfaceTypes)EditorGUILayout.EnumPopup("Surface Type", m_CellSurfaceType);

                GUI.color = Color.red;
                if (GUILayout.Button("Stop Paint Cells"))
                    m_IsPaintCell = false;
            }
        }

        private void ShowAddCheckPointsButton()
        {
            if (!m_IsAddingCheckpoints)
            {
                GUI.color = Color.green;
                if (GUILayout.Button("Start Add Checkpoints"))
                    m_IsAddingCheckpoints = true;
            }
            else
            {
                m_CheckPointType = (CheckPointTypes)EditorGUILayout.EnumPopup("Check Point Type", m_CheckPointType);

                GUI.color = Color.red;
                if (GUILayout.Button("Stop Add Checkpoints"))
                    m_IsAddingCheckpoints = false;
            }
        }

        private void ShowRemoveCheckPointsButton()
        {
            if (!m_IsRemovingCheckpoints)
            {
                GUI.color = Color.green;
                if (GUILayout.Button("Start Remove Checkpoints"))
                    m_IsRemovingCheckpoints = true;
            }
            else
            {
                GUI.color = Color.red;
                if (GUILayout.Button("Stop Remove Checkpoints"))
                    m_IsRemovingCheckpoints = false;
            }
        }

        private void OnSceneGUI()
        {
            if (m_IsPaintCell || m_IsAddingCheckpoints || m_IsRemovingCheckpoints)
            {
                switch (Event.current.type)
                {
                    case EventType.MouseDown:
                        {
                            if (Event.current.button == 0)
                            {
                                Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                                RaycastHit hitInfo;

                                if (Physics.Raycast(ray, out hitInfo))
                                {
                                    MapCellParam cellParam = hitInfo.collider.GetComponent<MapCellParam>();

                                    if (cellParam != null)
                                    {
                                        if (m_IsPaintCell)
                                        {
                                            if (!cellParam.CellType.Equals(m_PaintCellType))
                                            {
                                                MapCreator obj = (MapCreator)target;
                                                obj.UpdateMapCellValue(cellParam.X, cellParam.Y, (byte)m_PaintCellType, (byte)m_CellSurfaceType);

                                                m_IsEdited = true;
                                            }
                                        }

                                        if (m_IsAddingCheckpoints)
                                        {
                                            MapCreator obj = (MapCreator)target;
                                            if (!obj.HasCheckPointAt(cellParam.X, cellParam.Y))
                                                obj.AddCheckPoint(cellParam.X, cellParam.Y, (byte)m_CheckPointType);
                                        }

                                        if (m_IsRemovingCheckpoints)
                                        {
                                            MapCreator obj = (MapCreator)target;
                                            obj.RemoveCheckPoint(cellParam.X, cellParam.Y);
                                        }
                                    }
                                }
                            }

                            break;
                        }

                    case EventType.Layout:
                        {
                            int controlID = GUIUtility.GetControlID(GetHashCode(), FocusType.Passive);
                            HandleUtility.AddDefaultControl(controlID);
                            break;
                        }
                }
            }
        }
    }
}