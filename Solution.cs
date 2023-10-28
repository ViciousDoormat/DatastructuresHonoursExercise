using System;
using System.Collections.Generic;

class Program
{
	static void Main(string[] args)
	{
		Solution();
	}
	public static void Solution()
	{
		//input handling
		int steps = int.Parse(Console.ReadLine()); //4
		string[] input = Console.ReadLine().Split(' '); //3512476

		//input parsing
		Dictionary<int, Node> list = new Dictionary<int, Node>();
		int start = int.Parse(input[0].ToString());
		Node currentNode = new Node(start);
		Node rememberFirst = currentNode;
		int max = start, min = start;
		for (int i = 1; i < input.Length; i++)
		{
			Node node = new Node(int.Parse(input[i].ToString()));
			if (node.value > max)
				max = node.value;
			if (node.value < min)
				min = node.value;
			currentNode.next = node;
			node.previous = currentNode;
			list.Add(currentNode.value, currentNode);
			currentNode = node;
		}
		currentNode.next = rememberFirst;
		rememberFirst.previous = currentNode;
		list.Add(currentNode.value, currentNode);

		//performing the steps
		currentNode = rememberFirst;
		for (long i = 0; i < steps; i++)
		{
			Node before = currentNode.previous, after = currentNode.next; //remember the nodes you remove from the circle

			//remove the nodes from the circle (action 1)
			currentNode.previous = before.previous;
			before.previous.next = currentNode;
			currentNode.next = after.next;
			after.next.previous = currentNode;

			//finding out what will be the final destination of the removed nodes (action 2)
			int volgende = currentNode.value + 1;
			if(volgende > max)
				volgende = min;
			while (volgende == before.value || volgende == after.value)
			{
				volgende++;
				if (volgende > max)
					volgende = min;
			}

			//adding the removed nodes back to the circle (action 3)
			before.next = after;
			after.previous = before;
			Node newPosition = list[volgende], afterNew = newPosition.next;
			newPosition.next = before;
			before.previous = newPosition;
			afterNew.previous = after;
			after.next = afterNew;

			//going to the next current node (action 4)
			currentNode = currentNode.next.next;
		}

		//giving the output
		Console.Write(rememberFirst.value + " ");
		currentNode = rememberFirst.next;
		while (currentNode != rememberFirst)
		{
			Console.Write(currentNode.value + " ");
			currentNode = currentNode.next;
		}
		Console.WriteLine();
	}

	class Node
	{
		public Node(int value)
		{
			this.value = value;
		}
		public int value;
		public Node next, previous;

		//helps debugging
		public override string ToString()
		{
			string output = "";
			if (previous != null)
				output += previous.value + " -> ";
			output += value;
			if (next != null)
				output += " -> " + next.value;
			return output;
		}
	}
}
