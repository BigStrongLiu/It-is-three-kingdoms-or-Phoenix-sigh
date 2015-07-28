using UnityEditor;
using UnityEngine;

public class FightGridComponent : ComponentBase
{
    public float GridXLength;    //单元格子宽
    public float GridZLength;    //单元格子长
    public byte XGridCount;
    public byte EnemyZGridCount;
    public byte PlayerZGridCount;
    public float BorderZLength;
    public GridData[,] EnemyGirds { get; private set; }
    public GridData[,] PlayerGirds { get; private set; }
    public bool DrawGrid;

    [SerializeField]
    private float m_CenterX;
    [SerializeField]
    private float m_CenterZ;

    #region MonoBehaviour methods

    void OnDrawGizmos()
    {
        if (this.DrawGrid)
        {
            this.DrawEnemyGrid();
            this.DrawPlayerGrid();
            this.DrawBorder();
        }
    }

    #endregion

    #region override methods

    protected override void Awake()
    {
        base.Awake();
        this.InitEnemyGrid();
        this.InitPlayerGrid();
        this.InitCenter();
    }

    #endregion

    #region private methods

    private void InitEnemyGrid()
    {
        this.EnemyGirds = new GridData[this.EnemyZGridCount, this.XGridCount];
        for (byte z = 0; z < this.EnemyZGridCount; z++)
        {
            for (byte x = 0; x < this.XGridCount; x++)
            {
                this.EnemyGirds[z, x] = new GridData() { ZGrid = z, XGrid = x };
            }
        }
    }

    private void InitPlayerGrid()
    {
        this.PlayerGirds = new GridData[this.PlayerZGridCount, this.XGridCount];
        for (byte z = 0; z < this.PlayerZGridCount; z++)
        {
            for (byte x = 0; x < this.XGridCount; x++)
            {
                this.PlayerGirds[z,x] = new GridData() { ZGrid = z,XGrid = x };
            }
        }
    }

    private void DrawEnemyGrid()
    {
        for (byte z = 0; z < this.EnemyZGridCount; z++)
        {
            float positionZ = (z + this.m_CenterZ) * this.GridZLength;
            for (byte x = 0; x < this.XGridCount; x++)
            {
                float positionX = (x - this.m_CenterX) * this.GridXLength;
                Vector3 center = new Vector3(positionX, 0, positionZ);
                Vector3 size = new Vector3(this.GridXLength, 0, this.GridZLength);
                Gizmos.color = Color.red;
                Gizmos.DrawCube(center, size);
                Gizmos.color = Color.black;
                Gizmos.DrawWireCube(center, size);
            }
        }
    }

    private void DrawPlayerGrid()
    {
        for (byte z = 0; z < this.PlayerZGridCount; z++)
        {
            float positionZ = -(z+this.m_CenterZ) * this.GridZLength;
            for (byte x = 0; x < this.XGridCount; x++)
            {
                float positionX = (x - this.m_CenterX) * this.GridXLength;
                Vector3 center = new Vector3(positionX, 0, positionZ);
                Vector3 size = new Vector3(this.GridXLength, 0, this.GridZLength);
                Gizmos.color = Color.green;
                Gizmos.DrawCube(center, size);
                Gizmos.color = Color.black;
                Gizmos.DrawWireCube(center, size);
            }
        }
    }

    private void DrawBorder()
    {
        Vector3 center = Vector3.zero;
        Vector3 size = new Vector3(this.GridXLength * this.XGridCount, 0, this.GridZLength);
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(center, size);
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(center, size);
    }

    [ContextMenu("InitCenter")]
    private void InitCenter()
    {
        this.m_CenterX = this.XGridCount / 2.0f - 0.5f;
        this.m_CenterZ = (this.BorderZLength / 2) / this.GridZLength + 0.5f;
    }

    #endregion

    #region public methods

    public Vector3 ConvertGridToPosition(GridData data, ActorType type)
    {
        float positionX = (data.XGrid - this.m_CenterX) * this.GridXLength; ; 
        float positionZ = (data.ZGrid + this.m_CenterZ) * this.GridZLength * ((type == ActorType.Enemy)?1:-1);
        return new Vector3(positionX,0,positionZ);
    }

    public GridData ConvertPositionToGrid(Vector3 position,ActorType type)
    {
        return new GridData();
    }

    public GridData ConvertIndexToGridData(byte index)
    {
        return new GridData()
        {
            ZGrid =(byte)( index / this.XGridCount),
            XGrid = (byte)(index%this.XGridCount)
        };
    }

    #endregion
}
