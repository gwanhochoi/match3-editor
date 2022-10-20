using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CustomGrid
{
    private Vector3[] m_row_line;
    private Vector3[] m_column_line;

    private int m_width_count;
    private int m_height_count;

    private int m_cell_width;
    private int m_cell_height;

    private float start_x;
    private float start_y;
    private float end_x;
    private float end_y;


    public void Initialize(int width_count, int height_count, int cell_width, int cell_height)
    {
        this.m_width_count = width_count;
        this.m_height_count = height_count;
        this.m_cell_width = cell_width;
        this.m_cell_height = cell_height;

        int row_dot_count = (height_count + 1) * 2;
        int column_dot_count = (width_count + 1) * 2;

        //int view_width = width_count * cell_width;
        //int view_height = height_count * cell_height;

        m_row_line = new Vector3[row_dot_count];
        m_column_line = new Vector3[column_dot_count];


        start_x = -(width_count * cell_width) / 2.0f;
        end_x = -1 * start_x;
        start_y = -(height_count * cell_height) / 2.0f;
        end_y = -1 * start_y;

        int index = 0;
        for(int i = 0; i < row_dot_count / 2; i++)
        {
            m_row_line[index] = new Vector3(start_x, start_y + i * cell_height);
            m_row_line[++index] = new Vector3(end_x, start_y + i * cell_height);
            index++;

        }
        index = 0;
        for (int i = 0; i < column_dot_count / 2; i++)
        {
            m_column_line[index] = new Vector3(start_x + i * cell_width, start_y);
            m_column_line[++index] = new Vector3(start_x + i * cell_height, end_y);
            index++;

        }

        

    }

    public Vector3[] Get_RowLineVec()
    {
        return m_row_line;
    }

    public Vector3[] Get_ColumnLineVec()
    {
        return m_column_line;
    }


    public Vector2 Get_Current_Selected_Tile_Pos(Vector2 mousePos)
    {
        //left, bottom (0,0)
        //맵 밖에 있으면 무시
        //좌측하단 좌표 기준으로 cell 사이즈로 계산

        int posx = (int)((mousePos.x - start_x) / m_cell_width);
        int posy = (int)((mousePos.y - start_y) / m_cell_height);

        Debug.Log("tile_pos_x = " + posx);
        Debug.Log("tile_pos_y = " + posy);

        return new Vector2(posx, posy);


    }
}
