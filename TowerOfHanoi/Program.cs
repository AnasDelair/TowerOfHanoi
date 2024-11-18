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
                Size = diskSize;
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

            Console.WriteLine(Towers[0].TryPeek(out _));
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

                if (currentDisk.Size > disk.Size) return false;

                return true;

            } else if (isEmpty(Towers[towerIndex]))
                return true;

            return false;
        }

        static void moveDisk(List<Stack<Disk>> Towers, Disk disk, int towerIndex)
        {
            Towers[towerIndex].Push(disk);
        }

    }
}
