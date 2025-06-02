using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static string path = "tasks.txt";
    static void Main()
    {
        while (true)
        {

            Console.WriteLine("Welcome to Task Manager 📝!");

            Console.WriteLine("1. Add New Task");
            Console.WriteLine("2. Show All Tasks");
            Console.WriteLine("3. Edit Task");
            Console.WriteLine("4. Delete Task");
            Console.WriteLine("5. Mark/Unmark A Task");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();


            switch (choice)
            {
                case "1":

                    AddTask();
                    break;
                case "2":
                    ShowTasks();
                    break;
                case "3":
                    EditTask();
                    break;
                case "4":
                    DeleteTask();
                    break;
                case "5":
                    MarkTask();
                    break;
                case "6":
                    return;
                    break;
                default:
                    Console.WriteLine("Invalid Choice!");
                    break;
            }
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
    }
    static void AddTask()
    {
        Console.Write("Enter New Task:");
        string task = Console.ReadLine();
        List<string> lines = new List<string>();
        if (File.Exists(path))
        {
            lines.AddRange(File.ReadAllLines(path));
        }
        int id = lines.Count + 1;
        lines.Add($"{id}|{task}");
        File.WriteAllLines(path, lines);
        Console.WriteLine("✅ Task Added!");
    }
    static void ShowTasks()
    {
        if (!File.Exists(path))
        {
            Console.WriteLine("No Task Found!");
            return;
        }
        string[] lines = File.ReadAllLines(path);
        Console.WriteLine("Your Current Tasks:");
        foreach (string line in lines)
        {
            string[] parts = line.Split('|');
            Console.WriteLine($"{parts[0]}. {parts[1]}"); /* <-- To show the tasks as "X. Task Name" */
        }
    }

    static void EditTask()
    {
        ShowTasks();
        Console.Write("\nEnter task number to edit: ");
        string number = Console.ReadLine();

        List<string> lines = new List<string>(File.ReadAllLines(path));
        bool found = false;
        for (int i = 0; i < lines.Count; i++)
        {
            if (lines[i].StartsWith(number + "|"))
            {
                Console.Write("Enter a new description for the task: ");
                string newText = Console.ReadLine();
                lines[i] = number + "|" + newText;
                found = true;
                break;
            }

        }
        if (found)
        {
            File.WriteAllLines(path, lines);
            Console.WriteLine("✅ Task updated!");
        }
        else
        {
            Console.WriteLine("Task not found!");
        }
    }

    static void DeleteTask()
    {
        ShowTasks();
        Console.Write("Enter the number of task to delete:");
        string number = Console.ReadLine();
        List<string> lines = new List<string>(File.ReadAllLines(path));
        bool deleted = false;

        lines.RemoveAll(line =>
        {
            if (line.StartsWith(number + "|"))
            {
                deleted = true;
                return true;
            }
            return false;
        });
        if (deleted)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                string[] parts = lines[i].Split('|');
                lines[i] = (i + 1) + "|" + parts[1];
            }
            File.WriteAllLines(path, lines);
            Console.WriteLine("🗑️ Task deleted!");
        }
        else
        {
            Console.WriteLine("Task not found!");
        }

    }

    static void MarkTask()
    {
        ShowTasks();
        Console.WriteLine("Enter task number to mark/unmark:");
        string number = Console.ReadLine();
        List<string> lines = new List<string>(File.ReadAllLines(path));
        bool marked = false;
        bool unmarked = false;
        for (int i = 0; i < lines.Count; i++)
        {
            if (lines[i].StartsWith(number + "|"))
            {
                if (lines[i].Contains("[X]"))
                {
                    string[] parts = lines[i].Split("|");
                    string id = parts[0];
                    string[] textParts = parts[1].Split("]");
                    string text = textParts[1];

                    lines[i] = id + "|" + text;
                    unmarked = true;


                }
                else
                {
                    string[] parts = lines[i].Split("|");
                    string id = parts[0];
                    string text = parts[1];
                    lines[i] = id + "|" + " [X] " + text;
                    marked = true;
                }
            }


        }
        File.WriteAllLines(path, lines);
        if (marked)
        {
            Console.WriteLine("Task Marked!");
        }
        else if (unmarked)
        {
            Console.WriteLine("Task Unmarked!");
        }
        else
        {
            Console.WriteLine("Task Not Found!");
        }

    }
}