using System;
using System.Collections.Generic;
using System.Threading;

namespace BergerMT
{
    public class Manageur
    {
        private static readonly Dictionary<Farming, Func<AMovingItem>> actions;

        static Manageur()
        {
            actions = new Dictionary<Farming, Func<AMovingItem>>();
            actions[Farming.Wolf] = () => new Wolf();
            actions[Farming.Goat] = () => new Goat();
            actions[Farming.Cabbage] = () => new Cabbage();
        }

        #region Fields and constructors
        private Dictionary<Farming, AMovingItem> listUsers;
        private Dictionary<Farming, Thread>      listThreads;
        
        public Manageur()
        {
            /***********************
             * You can change this
             ***********************/
            var listPrey = new Dictionary<Farming, Farming>
            {
                {Farming.Farmer , Farming.Unknown   },
                {Farming.Wolf   , Farming.Goat      },
                {Farming.Goat   , Farming.Cabbage   },
                {Farming.Cabbage, Farming.Unknown   }
            };

            /******************************************
             * Initialize the OnPositionChanged event *
             ******************************************/
            Func<LinkedList<ActionDetail>> onPositionChanged = delegate
            {
                /*****************************
                 * TODO : Update UI
                 *****************************/

                /******************************
                 * Try to eat others animals
                 *****************************/
                // get farmer position
                AMovingItem farmer;
                var listAction = new LinkedList<ActionDetail>();
                if (this.listUsers.TryGetValue(Farming.Farmer, out farmer))
                {
                    foreach (var user in this.listUsers.Values)
                    {
                        var preys = user.listPrey;
                        if (preys.Count > 0)
                        {
                            Console.WriteLine("Asking to " + user.Id.ToString() + " to eat !");

                            listAction.AddLast(
                                user.CanTryEat(farmer.CurrentPosition)
                            );
                        }
                    }
                }
                return listAction;
            };

            /************************************
             * Auto initialize private fields
             ************************************/
            // list of all users
            Constants.DisplayMsg("create list of users");
            listUsers = new Dictionary<Farming, AMovingItem>();
            foreach (Farming classID in listPrey.Keys)
            {
                listUsers.Add(classID, this.MyFactory(classID));
            }

            // list of all threads
            Constants.DisplayMsg("Create threads for each user");
            listThreads = new Dictionary<Farming, Thread>();
            foreach (Farming classID in listPrey.Keys)
            {
                Thread thread = new Thread(listUsers[classID].WaitAction)
                {
                    Name = "Thread for user " + classID.ToString()
                };
                listThreads.Add(classID, thread);
            }

            // Add list of prey to each user
            foreach (var userId in this.listUsers.Keys)
            {
                // Find the prey of this user
                Farming preyID;
                listPrey.TryGetValue(userId, out preyID);

                AMovingItem prey;
                if (listUsers.TryGetValue(preyID, out prey))
                {
                    var user = listUsers[userId];
                    user.AddPrey(prey);
                }
            }

            // Add delegate to each user so they can notify a position change
            foreach (var user in this.listUsers.Values)
            {
                user.OnPositionChanged = onPositionChanged;
            }
        }
        #endregion


        #region Run program
        public void Run()
        {
            // start all threads
            this.StartThreads();
            Thread.Sleep(500);
            Console.WriteLine();

            // Get the list of actions to perform
            var parameters = Orchestrator.GetAction();

            // Run each action
            foreach (var parameter in parameters)
            {
                AMovingItem with;
                this.listUsers.TryGetValue(parameter.With, out with);

                Console.WriteLine("Processing action to {0}, with {1}",
                    this.listUsers[parameter.To], with);

                // Creation de l'action
                AMovingItem user = null;
                if (this.listUsers.TryGetValue(parameter.To, out user))
                {
                    ActionDetail action = new ActionDetail(
                        (a, b) => a.Move(b),
                        parameter
                    );

                    // Set the action to run
                    user.ActionToDo = action;

                    // Wait for the action to end
                    action.EndEvent.WaitOne();
                }
                else
                {
                    Console.WriteLine("Unknown user !");
                }

                Console.WriteLine();
            }

            // end all threads
            this.EndThreads();
        }
        #endregion


        #region Create and Get users
        public AMovingItem GetUser(Farming userId)
        {
            AMovingItem outValue = null;

            this.listUsers.TryGetValue(userId, out outValue);
            
            return outValue;
        }

        private AMovingItem MyFactory(Farming userId)
        {
            AMovingItem returnValue = null;

            if (userId == Farming.Farmer)
            {
                returnValue = new Farmer(this.listUsers);
            }
            else if(actions.ContainsKey(userId))
            {
                returnValue = actions[userId]();
            }

            return returnValue;
        }
        #endregion


        #region Start and End all threads
        private void StartThreads()
        {
            foreach (Thread thread in this.listThreads.Values)
            {
                if (thread.ThreadState == ThreadState.Unstarted)
                {
                    Console.WriteLine("starting " + thread.Name);
                    thread.Start();
                }
            }
        }

        public void EndThreads()
        {
            Console.WriteLine();
            Console.WriteLine("End threads...");
            foreach (var userID in this.listUsers.Keys)
            {
                var user = this.listUsers[userID];
                var thread = this.listThreads[userID];

                if (thread.ThreadState != ThreadState.Stopped)
                {
                    Console.WriteLine("Waiting for : " + thread.Name);

                    user.ThreadIsActive = false;

                    thread.Join();
                }
            }
        }
        #endregion


        public void RecordPosition(Farming userID, Position position)
        {
            Constants.DisplayMsg("moved to " + position);
        }


    }
}
