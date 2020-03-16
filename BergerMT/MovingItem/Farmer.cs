using System.Collections.Generic;
using System.Threading;

namespace BergerMT
{
    public class Farmer : AMovingItem
    {
        private readonly Dictionary<Farming, AMovingItem> listUsers;

        public Farmer(Dictionary<Farming, AMovingItem> users) : base(Farming.Farmer)
        {
            this.listUsers = users;
        }

        public override void Move(FarmingAction farmAction)
        {
            // Get user to move
            AMovingItem userToMove;
            this.listUsers.TryGetValue(farmAction.With, out userToMove);

            if (userToMove != null)
            {
                // Goat : river to boat
                var action = new ActionDetail(
                    (a,b) => a.MoveTo(b),
                    new FarmingAction
                    {
                        To = farmAction.To,
                        Direction = farmAction.Direction,
                        With = farmAction.With
                    }
                );
                userToMove.ActionToDo = action;
                action.EndEvent.WaitOne();
            }
            this.PurgeWaitingList();

            // Farmer : river to boat
            this.MoveTo(new FarmingAction
            {
                To = farmAction.To,
                Direction = farmAction.Direction,
                With = farmAction.With
            });
            this.PurgeWaitingList();

            // Farmer : boat to river
            this.MoveTo(new FarmingAction
            {
                To = farmAction.To,
                Direction = farmAction.Direction,
                With = farmAction.With
            });
            this.PurgeWaitingList();

            if (userToMove != null)
            {
                // Goat : river to boat
                var action = new ActionDetail(
                    (a, b) => a.MoveTo(b),
                    new FarmingAction
                    {
                        To = farmAction.To,
                        Direction = farmAction.Direction,
                        With = farmAction.With
                    }
                );
                userToMove.ActionToDo = action;
                action.EndEvent.WaitOne();
            }
            this.PurgeWaitingList();
        }
    }
}
