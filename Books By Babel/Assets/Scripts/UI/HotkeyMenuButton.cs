using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// SAFE TO DELETE
/// SAFE TO DELETE
/// SAFE TO DELETE
/// SAFE TO DELETE
/// SAFE TO DELETE
/// SAFE TO DELETE
/// SAFE TO DELETE
/// </summary>
public class HotkeyMenuButton : MonoBehaviour {


    BoardManager board;
    KeyBindingNames currBindings;

    public void InitButton(BoardManager board, KeyBindingNames currBindings)
    {
        this.board = board;
        this.currBindings = currBindings;
    }

	public void RebindKey()
    {
        board.inputFSM.SwitchState(new RebindState(board, currBindings));
    }
}
