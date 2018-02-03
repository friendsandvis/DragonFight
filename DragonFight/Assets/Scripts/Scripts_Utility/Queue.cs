using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedNode
{
	public Effect effect;
	public LinkedNode next;

	public LinkedNode (Effect eff,LinkedNode node)
	{
		this.effect = eff;
		this.next = node;
	}
}

//to be used with a linked node class only
public class LinkedQueue {
	LinkedNode first,last;

	public LinkedQueue()
	{
		first=last = null;
	}

	public void insert(LinkedNode data)
	{
		//if empty
		if (first == null) {
			first = data;
			last = data;
			return;
		}

		//if not empty
		last.next=data;
		last = data;
	}

	public LinkedNode delete()
	{
		if (first == null) {
			return null;
		}

		LinkedNode tempnaode = first;
		first = first.next;
		tempnaode.next = null;

		//check for beyound last case
		if (first == null) 
			last = null;

		return tempnaode;
	}
}
