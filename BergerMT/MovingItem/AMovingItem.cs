using System;
using System.Collections.Generic;
using System.Threading;

namespace BergerMT
{
    public abstract class AMovingItem : IDisposable
    {
        private static readonly Dictionary<string, Position> movingActions;

        static AMovingItem()
        {
            movingActions = new Dictionary<string, Position>();

            string posDir = Position.Left.ToString() + Direction.Right.ToString();
            movingActions[posDir] = Position.Boat;

            posDir = Position.Boat.ToString() + Direction.Left.ToString();
            movingActions[posDir] = Position.Left;

            posDir = Position.Boat.ToString() + Direction.Right.ToString();
            movingActions[posDir] = Position.Right;

            posDir = Position.Right.ToString() + Direction.Left.ToString();
            movingActions[posDir] = Position.Boat;
        }

        #region Fields and constructors
        public Farming Id { get; set; }

        private LinkedList<ActionDetail> waitingList;
        private ActionDetail waitingAction
        {
            get
            {
                ActionDetail action = null;
                if (waitingList.First != null)
                {
                    action = waitingList.First.Value;
                    waitingList.RemoveFirst();
                }
                return action;
            }
        }
        public Func<LinkedList<ActionDetail>> OnPositionChanged { get; set; }

        public LinkedList<AMovingItem> listPrey { get; set; }
        private Position currentPosition;
        public Position CurrentPosition
        {
            get { return currentPosition; }
            set
            {
                Console.WriteLine(this.Id.ToString() + " moved from " + currentPosition.ToString() + " to " + value.ToString());
                currentPosition = value;
                Console.WriteLine("Nombre d'éléments en attente : " + this.waitingList.Count);
                if (this.waitingList.Count > 0)
                {
                    Console.WriteLine("Actions en attente dans " + this.Id.ToString());
                    throw new SystemException("Internal error. Unexpected actions waiting");
                }
                this.waitingList = this.OnPositionChanged();
            }
        }


        public bool ThreadIsActive { get; set; }
        public EventWaitHandle StartHandle { get; set; }
        public EventWaitHandle EndHandle { get; set; }

        protected LinkedList<ActionDetail> ToDoList { get; set; }
        public ActionDetail ActionToDo
        {
            get
            {
                ActionDetail actionToDo = null;
                if (ToDoList.First != null)
                {
                    actionToDo = ToDoList.First.Value;
                    ToDoList.RemoveFirst();
                }
                return actionToDo;
            }
            set
            {
                if (value != null)
                {
                    Console.WriteLine(this.Id.ToString() + " adding new action to " + this.Id.ToString());
                    this.ToDoList.AddLast(value);
                    this.StartHandle.Set();
                }
                else
                {
                    Console.WriteLine("trying to add null action");
                }
            }
        }

        internal AMovingItem(Farming userID)
        {
            this.Id = userID;

            ThreadIsActive = true;
            this.listPrey = new LinkedList<AMovingItem>();

            this.StartHandle = new ManualResetEvent(false);
            this.EndHandle = new AutoResetEvent(false);

            this.ToDoList = new LinkedList<ActionDetail>();
            this.currentPosition = Position.Left;
            this.waitingList = new LinkedList<ActionDetail>();
        }
        #endregion


        #region Eating and Prey
        public void AddPrey(AMovingItem prey)
        {
            this.listPrey.AddLast(prey);
        }
        
        public void TryEat(FarmingAction farmingAction)
        {
            EatAction action = (EatAction) farmingAction;
            Position farmerPosition = action.FarmerPosition;

            foreach (var myPrey in this.listPrey)
            {
                var preyPosition = myPrey.CurrentPosition;

                if (this.currentPosition != preyPosition)
                {
                    // can't eat !
                }
                else if (this.CurrentPosition != farmerPosition)
                {
                    // can't eat again
                }
                else
                {
                    Console.WriteLine(this.Id.ToString() + " Miammmmmmmmmmmmmmmmmmmmm !");
                    Console.WriteLine("Je mange ma proie");
                }
            }
        }

        public ActionDetail CanTryEat(Position farmerPosition)
        {
            var action = new ActionDetail(
                (a, b) => a.TryEat(b),
                new EatAction
                {
                    To = this.Id,
                    With = Farming.Unknown,
                    Direction = Direction.Unknwon,
                    FarmerPosition = farmerPosition
                }
            );
            Console.WriteLine("Ajout de l'action " + action.N + " a la waiting list");


            this.ToDoList.AddFirst(action);
            this.StartHandle.Set();

            // */
            return action;
        }
        #endregion


        #region Do / Wait action
        public void DoAction(ActionDetail action)
        {
            // Run action
            action.FunctionToCall(this, action.Parameters);

            // signal end of processing
            action.EndEvent.Set();
        }

        private bool HasAction()
        {
            return this.ToDoList.First != null;
        }

        public void WaitAction()
        {
            while (ThreadIsActive)
            {
                // Wait for new action
                this.StartHandle.WaitOne(15000);
                this.StartHandle.Reset();

                // for each action
                ActionDetail action;
                while (this.HasAction())
                {
                    action = this.ActionToDo;
                    Console.WriteLine("Execution de l'action " + action.N);
                    this.DoAction(action);
                }

                action = this.waitingAction;
                while (action != null)
                {
                    Console.WriteLine("Running a waiting action " + action.N);
                    this.DoAction(action);
                    action = this.waitingAction;
                }
            }
        }

        protected void PurgeWaitingList()
        {
            while (this.waitingList.First != null)
            {
                var action = waitingList.First.Value;
                waitingList.RemoveFirst();
                action.EndEvent.WaitOne();
            }
        }
        #endregion


        #region Move functions
        public void MoveTo(FarmingAction action)
        {
            Direction direction = action.Direction;

            string positionDirection = this.currentPosition.ToString() + direction.ToString();
            if (movingActions.ContainsKey(positionDirection))
            {
                this.currentPosition = movingActions[positionDirection];
            }
            else
            {
                throw new ArgumentException("Direction not available");
            }
        }

        public virtual void Move(FarmingAction o)
        {
            throw new NotImplementedException();
        }
        #endregion


        #region Display functions
        public virtual void DisplayData(object o)
        {
            List<string> list = (List<string>)o;
            Console.WriteLine("    I am thread " + Thread.CurrentThread.ManagedThreadId);
            foreach (var content in list)
            {
                Console.WriteLine(content);
            }
            Console.WriteLine();
        }
        #endregion

        public void Dispose()
        {
            this.StartHandle.Dispose();
            this.EndHandle.Dispose();
        }
    }
}
