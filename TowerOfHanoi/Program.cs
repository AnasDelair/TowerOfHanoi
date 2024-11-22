namespace TowerOfHanoi
{
    internal class Program
    {
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
            List<Stack<Disk>> Towers = new List<Stack<Disk>>();

            Towers.Add(new Stack<Disk>());
            Towers.Add(new Stack<Disk>());
            Towers.Add(new Stack<Disk>());

            Stack<int> teststack = new Stack<int>();
            teststack.Push(0);

            Disk Disk1 = new Disk(3, 1);
            Disk Disk2 = new Disk(2, 2);
            Disk Disk3 = new Disk(1, 3);

            Towers[Disk1.Tower].Push(Disk1);
            Towers[Disk2.Tower].Push(Disk2);
            Towers[Disk3.Tower].Push(Disk3);

            Disk3 = moveDisk(Towers, Disk3, 1);

            // Console.WriteLine((Towers[Disk3.Tower].Peek().Size == Disk3.Size));
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
            if (canPeek(Towers[towerIndex]) && !isEmpty(Towers[towerIndex]))
            {
                Disk currentDisk = Towers[towerIndex].Peek();
                Disk topMostDisk = Towers[disk.Tower].Peek();

                if (topMostDisk.Size != disk.Size)
                {
                    Console.WriteLine("Trying to move a disk that isnt at the top of the tower.");
                    return false; // disk you are moving has to be the topmost disk
                }
                if (currentDisk.Size > disk.Size)
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

            } else if (isEmpty(Towers[towerIndex]))
            {
                Disk topMostDisk = Towers[disk.Tower].Peek();
                return topMostDisk.Size == disk.Size;
            }

            return false;
        }

        static Disk moveDisk(List<Stack<Disk>> Towers, Disk disk, int towerIndex)
        {
            if (isValid(Towers, disk, towerIndex))
            {
                Console.WriteLine("Valid Move!");

                Disk newDisk = new Disk(disk.Size / 2, disk.HeightIndex);
                newDisk.Tower = towerIndex;

                Towers[disk.Tower].Pop();
                Towers[towerIndex].Push( newDisk ); // take out the current disk, and put it in the new tower

                Console.WriteLine($"Disk {newDisk.Size / 2} moved to tower {newDisk.Tower}!");

                displayHanoi(Towers);
                return newDisk;
            }
            Console.WriteLine("Invalid move!");
            return disk;
        }

        static void displayHanoi(List<Stack<Disk>> Towers)
        {
            foreach (Stack<Disk> Tower in Towers)
            {
                Console.WriteLine();
                foreach (Disk disk in Tower)
                {
                    string diskString = "<" + new string('=', disk.Size) + ">";
                    diskString = diskString.Insert(diskString.Length / 2, "|");
                    diskString = new string(' ', disk.Size) + diskString;
                    Console.WriteLine(diskString);
                }
            }
        }

    }
}
