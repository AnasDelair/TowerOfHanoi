namespace TowerOfHanoi
{
    internal class Program
    {
        public static class Game
        {
            public static int moves = 0;
            public static bool gameEnded = false;

            public static int TowerNumber = 3;
            public static int DiskNumber = 3;
            public static int startingTower = 0;

            public static int maxSize = DiskNumber * 2;
            public static int maxHeight = DiskNumber + 1;

            public static int smallestDisk = 1;
            public static int biggestDisk = DiskNumber;
        }

        struct Disk
        {
            public int Tower;
            public int HeightIndex;
            public int Size;

            public Disk(int diskSize, int heightIndex)
            {
                Tower = 0;
                HeightIndex = heightIndex;
                Size = diskSize * 2;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("How many towers do you want to play with?");

            Game.TowerNumber = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("How many disks do you want to play with?");

            Game.DiskNumber = Convert.ToInt32(Console.ReadLine());

            List<Stack<Disk>> Towers = new List<Stack<Disk>>();

            for (int i = 1; i <= Game.TowerNumber; i++)
            {
                Towers.Add(new Stack<Disk>());
            }

            for (int i = 1; i <= Game.DiskNumber; i++)
            {
                Towers[Game.startingTower].Push(new Disk((Game.DiskNumber + 1) - i, i));
            }
            
            displayHanoi(Towers);

            while (!Game.gameEnded)
            {
                Console.WriteLine($"Which disk would you like to move? smallest: {Game.smallestDisk} till largest: {Game.biggestDisk}");
                int diskToMove = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine($"Which Tower would you like to move to? smallest: 1 till largest: {Game.TowerNumber}");
                int towerToMoveTo = Convert.ToInt32(Console.ReadLine()) - 1;

                moveDisk(Towers, diskToMove, towerToMoveTo);
            }
        }

        static bool isEmpty(Stack<Disk> stack)
        {
            return stack.Count == 0;
        }

        static bool canPeek(Stack<Disk> Tower)
        {
            return Tower.TryPeek(out _);
        }

        static bool isValid(List<Stack<Disk>> Towers, Disk disk, int towerIndex)
        {
            if (towerIndex >= Game.TowerNumber)
            {
                Console.WriteLine($"Tower {towerIndex} doesnt exist!");
                return false;
            }

            if (canPeek(Towers[towerIndex]) && !isEmpty(Towers[towerIndex]))
            {
                Disk currentDisk = Towers[towerIndex].Peek();
                Disk topMostDisk = Towers[disk.Tower].Peek();

                if (topMostDisk.Size != disk.Size)
                {
                    Console.WriteLine("Trying to move a disk that isnt at the top of the tower.");
                    return false; // disk you are moving has to be the topmost disk
                }
                if (currentDisk.Size < disk.Size)
                {
                    Console.WriteLine("Trying to move the disk to a tower with a smaller disk on it.");
                    return false; // size check
                }
                if (disk.Tower == towerIndex)
                {
                    Console.WriteLine("Trying to move a disk to the tower that its already on.");
                    return false; // cant move to same tower
                }

                return true;

            }
            else if (isEmpty(Towers[towerIndex]))
            {
                Disk topMostDisk = Towers[disk.Tower].Peek();
                return topMostDisk.Size == disk.Size;
            }

            return false;
        }

        static void moveDisk(List<Stack<Disk>> Towers, int diskID, int towerIndex)
        {
            Disk disk = new Disk();
            bool found = false;
            foreach (Stack<Disk> Tower in Towers)
            {
                if (found) break;
                foreach (Disk thisDisk in Tower)
                {
                    if (thisDisk.Size == diskID * 2)
                    {
                        disk = thisDisk;
                        found = true;
                        break;
                    }
                }
            }

            if (!found)
            {
                Console.WriteLine("Desired disk to move couldn't be found.");
                return;
            }

            if (isValid(Towers, disk, towerIndex))
            {
                Console.WriteLine("Valid Move!");
                Game.moves += 1;
                Console.WriteLine($"Moves made: {Game.moves}");

                Disk newDisk = new Disk(disk.Size / 2, disk.HeightIndex);
                newDisk.Tower = towerIndex;

                Towers[disk.Tower].Pop();
                Towers[towerIndex].Push(newDisk); // take out the current disk, and put it in the new tower

                Console.WriteLine($"Disk {newDisk.Size / 2} moved to tower {newDisk.Tower}!");
                disk = newDisk;

                displayHanoi(Towers);

                checkGameState(Towers);
                return;
            }
            Console.WriteLine("Invalid move!");
        }

        static void displayHanoi(List<Stack<Disk>> Towers)
        {
            int maxSize = Game.maxSize;
            int maxHeight = Game.maxHeight;

            Console.WriteLine();
            foreach (Stack<Disk> Tower in Towers)
            {
                if (Tower.Count > 0)
                {
                    for (int i = 0; i <= maxHeight - Tower.Count; i++)
                    {
                        Console.WriteLine(new string(' ', maxSize + 1) + "|");
                    }
                    foreach (Disk disk in Tower)
                    {
                        string diskString = "<" + new string('=', disk.Size) + ">";
                        diskString = diskString.Insert(diskString.Length / 2, "|");
                        diskString = new string(' ', maxSize - disk.Size / 2) + diskString;
                        Console.WriteLine(diskString);
                    }
                    Console.WriteLine(new string('-', maxSize * 2 + maxSize / 2));
                    Console.WriteLine();
                }
                else
                {
                    for (int i = 0; i <= maxHeight; i++)
                    {
                        Console.WriteLine(new string(' ', maxSize + 1) + "|");
                    }
                    Console.WriteLine(new string('-', maxSize * 2 + maxSize / 2));
                    Console.WriteLine();
                }
            }
        }

        static void checkGameState(List<Stack<Disk>> Towers)
        {
            foreach (Stack<Disk> Tower in Towers)
            {
                if (Tower.Count == Game.DiskNumber)
                {
                    Game.gameEnded = true;
                    Console.WriteLine("You Won!");
                }
            }
        }

        /*
        static void solveHanoi(List<Stack<Disk>> Towers)
        {
            if (Game.DiskNumber % 2 == 1)
            {
                Console.WriteLine("Solving Hanoi Even Method");

                int storageTower = 1;
                int targetTower = Game.TowerNumber - 1;
                int currentDisk = Game.smallestDisk;

                for (int i = 1; i <= 7; i++)
                {
                    moveDisk(Towers, currentDisk, storageTower);
                    currentDisk += 1;
                    storageTower += 1;
                }
            }
        }
        */

    }
}
