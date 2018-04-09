using UnityEngine;
using System.Collections;

// Subclass for selecting oldest cell from the container
public class OldestTreeMazeGenerator : TreeMazeGenerator {
	public OldestTreeMazeGenerator(int row, int column):base(row,column){
		
	}
	
	protected override int GetCellInRange(int max)
	{
		return 0;
	}
}// End of OldestTreeMazeGenerator