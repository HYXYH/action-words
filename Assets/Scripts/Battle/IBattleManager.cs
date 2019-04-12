using System;
using System.Collections.Generic;

namespace Battle
{
   public interface IBattleManager
   {
      void StartBattle();

      // True if player won
      void SetBattleEndCallback(Action<bool> callback);
      
   }
}
