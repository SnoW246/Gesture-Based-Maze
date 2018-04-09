using UnityEngine;
using System.Collections;

// Selecting last cell from the container. Result equal to Recursive algorithm, 
// so TreeMazeGenerator becomes non-recursive realisation of RecursiveGenerator
public class RecursiveTreeMazeGenerator : TreeMazeGenerator {
	public RecursiveTreeMazeGenerator(int row, int column):base(row,column){
		
	}
	
	protected override int GetCellInRange(int max){
		return max;
	}
}// End of RecursiveTreeMazeGenerator