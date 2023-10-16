using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject {

    private GridSystem<GridObject> gridSystem;
    private GridPosition gridPosition;
    private List<Unit> unitList;
    public Door door {get; private set;}

    public GridObject(GridSystem<GridObject> gridSystem, GridPosition gridPosition) {
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

    public Unit GetFirstUnit() {
        if (HasAnyUnit()) {
            return unitList[0];
        } else {
            return null;
        }
    }

    public void SetDoor(Door door) {
        this.door = door;
    }
}