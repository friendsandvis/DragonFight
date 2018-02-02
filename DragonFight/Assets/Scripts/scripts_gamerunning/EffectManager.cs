using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EffectManager {
	public LinkedQueue lq_all;
	public LinkedQueue[] lq_players;

	public EffectManager(uint nplayers)
	{
		lq_all = new LinkedQueue ();
		lq_players = new LinkedQueue[nplayers];
		for(int z=0;z<nplayers;z++)
			lq_players[z]=new LinkedQueue();
	}

	public void InitiateEffect(Effect effect,int effectedplayer)
	{
		if (effectedplayer == -1)
			lq_all.insert (new LinkedNode (effect, null));
		else
			lq_players [effectedplayer].insert (new LinkedNode (effect, null));
	}


	//apply effect all
	public void applyEffectsAll()
	{
		LinkedNode	effectnode;
		LinkedQueue lqnew=new LinkedQueue();

		//apply effects
		while ((effectnode = lq_all.delete ()) != null) {
			effectnode.effect.effect ();

			if (!effectnode.effect.isComplete ())
				lqnew.insert(effectnode);
		}

		lq_all = lqnew;

	}

	//apply effect per player
	public void applyEffects(uint playerindex)
	{
		LinkedNode	effectnode;
		LinkedQueue lqnew=new LinkedQueue();

		//apply effects
		while ((effectnode = lq_players[playerindex].delete ()) != null) {
			effectnode.effect.effect ();

			if (!effectnode.effect.isComplete ())
				lqnew.insert(effectnode);
		}

		lq_players[playerindex] = lqnew;

	}
}
