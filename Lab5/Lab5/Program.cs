using System;

class Node
{
    public char Value;
    public Node Next;

    public Node(char value)
    {
        Value = value;
    }
}

class Stack
{
    private Node head;

    public bool IsEmpty()
    {
        return head == null;
    }

    public void Push(char value)
    {
        Node n = new Node(value);
        n.Next = head;
        head = n;
        Print();
    }

    public char Pop()
    {
        if (IsEmpty()) throw new Exception("Стек порожній");

        char val = head.Value;
        head = head.Next;
        Print();
        return val;
    }

    public char Peek()
    {
        return head.Value;
    }

    public void Print()
    {
        Console.Write("Стек: ");

        if (head == null)
        {
            Console.WriteLine("порожній");
            return;
        }

        for (Node cur = head; cur != null; cur = cur.Next)
        {
            Console.Write(cur.Value + " ");
        }

        Console.WriteLine();
    }
}

class Program
{
    static int Priority(char c)
    {
        if (c == '+' || c == '-') return 1;
        if (c == '*' || c == '/') return 2;
        return 0;
    }

    static string ToRPN(string s)
    {
        Stack st = new Stack();
        string res = "";
        int balance = 0;

        for (int i = 0; i < s.Length; i++)
        {
            char c = s[i];

            if (c == ' ') continue;

            if (char.IsDigit(c))
            {
                string number = "";

                for (; i < s.Length && char.IsDigit(s[i]); i++)
                {
                    number += s[i];
                }

                i--;

                res += number + " ";
            }
            else if (char.IsLetter(c))
            {
                res += c + " ";
            }
            else if (c == '(')
            {
                st.Push(c);
                balance++;
            }
            else if (c == ')')
            {
                balance--;
                if (balance < 0) throw new Exception("Помилка");

                while (!st.IsEmpty() && st.Peek() != '(')
                {
                    res += st.Pop() + " ";
                }

                st.Pop();
            }
            else if (c == '+' || c == '-' || c == '*' || c == '/')
            {
                while (!st.IsEmpty() && Priority(st.Peek()) >= Priority(c))
                {
                    res += st.Pop() + " ";
                }

                st.Push(c);
            }
            else
            {
                throw new Exception("Недопустимий символ");
            }
        }

        if (balance != 0) throw new Exception("Помилка");

        while (!st.IsEmpty())
        {
            char t = st.Pop();
            if (t == '(') throw new Exception("Помилка");
            res += t + " ";
        }

        return res;
    }

    static void Run(string input)
    {
        try
        {
            Console.WriteLine("Вираз: " + input);
            string rpn = ToRPN(input);
            Console.WriteLine("RPN: " + rpn);
        }
        catch (Exception e)
        {
            Console.WriteLine("Помилка: " + e.Message);
        }
    }

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;
        while (true)
        {
            Console.WriteLine("1 - Ввести вираз");
            Console.WriteLine("2 - Контрольний приклад");
            Console.WriteLine("0 - Вихід");

            string c = Console.ReadLine();

            if (c == "1")
            {
                Console.Write("Введіть вираз: ");
                string input = Console.ReadLine();
                Run(input);
            }
            else if (c == "2")
            {
                Run("3*x*y+(y-5/(x*y))");
            }
            else if (c == "0")
            {
                break;
            }
            else
            {
                Console.WriteLine("Невірний вибір");
            }

            Console.WriteLine();
        }
    }
}
