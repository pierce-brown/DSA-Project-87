using System;
using System.Collections.Generic;
using System.Diagnostics;

// Class that represents a single product in the dataset
class Product
{
    // Each product has an ID, quantity, and price. They are sorted by ID
    public int ID;
    public int Quantity;
    public double Price;

    public Product(int id, int quantity, double price)
    {
        ID = id;
        Quantity = quantity;
        Price = price;
    }
}

class Program
{
    // Vars that track how many operations the algorithm performs
    static long comparisons = 0;
    static long swaps = 0;

    static void Main()
    {
        // testing quick sort for each dataset size
        Console.WriteLine("Quick Sort: 100 products");
        RunQuickSort(GenerateData(100));
        Console.WriteLine("\n");

        Console.WriteLine("Quick Sort: 1k products");
        RunQuickSort(GenerateData(1000));
        Console.WriteLine("\n");

        Console.WriteLine("Quick Sort: 10k products");
        RunQuickSort(GenerateData(10000));
        Console.WriteLine("\n");

        Console.WriteLine("Quick Sort: 100k products");
        RunQuickSort(GenerateData(100000));
        Console.WriteLine("\n");

        Console.WriteLine("Quick Sort: 500k products");
        RunQuickSort(GenerateData(500000));
        Console.WriteLine("\n");
    }



    // Function that generates a list of random Products
    static List<Product> GenerateData(int size)
    {
        Random rand = new Random();
        List<Product> data = new List<Product>();

        // Loop to create the requested number of products
        // Generates a random value for each field then adds it to the list
        for (int i = 0; i < size; i++)
        {
            // IDs go up to 1,000,000 for realism and to reduce duplicates
            int id = rand.Next(1, 1000000);
            int quantity = rand.Next(1, 1000);
            double price = rand.NextDouble() * 100;

            data.Add(new Product(id, quantity, price));
        }

        return data;
    }

    // Quick Sort function
    static void QuickSort(List<Product> arr, int low, int high)
    {
        // Only sort if there is more than one element
        if (low < high)
        {
            // Partition the array and get pivot position
            int pivotIndex = Partition(arr, low, high);

            // Recursively sort left and right sides respectively
            QuickSort(arr, low, pivotIndex - 1);
            QuickSort(arr, pivotIndex + 1, high);
        }
    }

    // Runs Quick Sort, measures operation count/execution time, and returns results
    static void RunQuickSort(List<Product> data)
    {
        comparisons = 0;
        swaps = 0;

        var watch = Stopwatch.StartNew();
        QuickSort(data, 0, data.Count - 1);
        watch.Stop();

        Console.WriteLine("Quick Sort Results:");
        Console.WriteLine($"Time: {watch.ElapsedMilliseconds} ms");
        Console.WriteLine($"Comparisons: {comparisons}");
        Console.WriteLine($"Swaps: {swaps}");
    }

    // Median of Three method to find better pivot
    static int MedianOfThree(List<Product> arr, int low, int high)
    {
        int mid = (low + high) / 2;

        // Compare and swap values to order low/mid/high
        if (arr[low].ID > arr[mid].ID) Swap(arr, low, mid);
        if (arr[low].ID > arr[high].ID) Swap(arr, low, high);
        if (arr[mid].ID > arr[high].ID) Swap(arr, mid, high);

        return mid;
    }

    // Partitions array around a pivot
    static int Partition(List<Product> arr, int low, int high)
    {
        // Select pivot and move it to end temporarily
        int pivotIndex = MedianOfThree(arr, low, high);
        Product pivot = arr[pivotIndex];
        Swap(arr, pivotIndex, high);

        int i = low - 1;

        // Loop through array and compare each element to pivot
        for (int j = low; j < high; j++)
        {
            comparisons++;

            // If current value is smaller than pivot, move it to the left side
            if (arr[j].ID < pivot.ID)
            {
                i++;
                Swap(arr, i, j);
            }
        }

        // Place pivot in correct sorted position
        Swap(arr, i + 1, high);

        return i + 1;
    }

    // Swaps two elements in the list
    static void Swap(List<Product> arr, int i, int j)
    {
        swaps++;

        Product temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }
}
