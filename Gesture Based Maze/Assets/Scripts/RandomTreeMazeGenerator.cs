using UnityEngine;
using System.Collections;

// Sublcass for selecting random cell from the container
public class RandomTreeMazeGenerator : TreeMazeGenerator {
	public RandomTreeMazeGenerator(int row, int column):base(row,column){
		
	}
	
	protected override int GetCellInRange(int max)
	{
		return Random.Range (0, max+1);
	}
}// End of RandomTreeMazeGenerator