using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject {

    private GridSystem gridSystem;
    private GridPosition gridPosition;
    private List<Unit> unitList;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition) {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        unitList = new List<Unit>();
    }

    public List<Unit> GetUnitList() {
        return unitList;
    }

    public void AddUnit(Unit unit) {
        unitList.Add(unit);
    }

    public void RemoveUnit(Unit unit) {
        unitList.Remove(unit);
    }

    public override string ToString() {
        string unitString = "";
        foreach (var unit in unitList) {
            unitString += unit + "\n";
        }

        return gridPosition.ToString() + "\n" + unitString;
    }

    public bool HasAnyUnit() {
        return unitList.Count > 0;
    }
}