using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
    public enum FitType
    {
        Uniform,
        Width,
        Height,
        FixedRows,
        FixedColumn


    };


    [Header("Grid Properties")]
    public int rows;
    public int column;
    [SerializeField] private Vector2 cellSize;
    [SerializeField] private Vector2 spacing;

    [SerializeField] private FitType fitType;
    public bool fitX;
    public bool fitY;


    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        if( fitType == FitType.FixedRows || fitType == FitType.FixedColumn || fitType == FitType.Uniform)
        {
            fitX = true;
            fitY = true;
            float sqrt = Mathf.Sqrt(transform.childCount);
            rows = Mathf.CeilToInt(sqrt);
            column = Mathf.CeilToInt(sqrt);
        }


        

        if (fitType == FitType.Width || fitType == FitType.FixedColumn)
        {
            rows = Mathf.CeilToInt(transform.childCount / (float)column);
        }

        if (fitType == FitType.Height || fitType == FitType.FixedRows)
        {
            column = Mathf.CeilToInt(transform.childCount / (float)rows);
        }


        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = (parentWidth - padding.left - padding.right - spacing.x * (column - 1)) / (float)column;
        float cellHeight = (parentHeight - padding.top - padding.bottom - spacing.y * (rows - 1)) / (float)rows;

        cellSize.x = fitX ? cellWidth: cellSize.x;
        cellSize.y = fitY ? cellHeight: cellSize.y;

        int columnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / column;
            columnCount = i % column;

            var item = rectChildren[i];

            var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
            var yPos = (cellSize.x * rowCount) + (spacing.y * rowCount) + padding.top; 

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        } 
    }

    public override void CalculateLayoutInputVertical()
    {
       
    }

    public override void SetLayoutVertical()
    {
       

    }

    public override void SetLayoutHorizontal()
    {
       
    }
}
