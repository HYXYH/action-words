using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoardGame
{
    // Activates prefab with board game
    void StartBoardGame();
    
    // Clear and hide board
    void EndBoardGame();

    
    
    void SetWordActivationCallback(Action<string, List<Thaum>> callback);

        
    void CallWordActivationCallback(string word, List<Thaum> thaums);

}
