unity game project Lycanthrosor(Lycanor) from 2026 (abandoned due to artistic limitations). a game where the player would face an opponent in 
turn based combat, both the player and enemies would have a set of resources which they would spend to complete actions, applying effects to 
enemies/themselves.

requirements :
  - OS that can run unity editor.
  - unity installed on machine to run the editor.
  - a unity version to run the project in.

install instructions :
    - download repository and unzip.
    - open unity hub and click add then add project from disk.
    - navigate and select unzipped repository.
    - you will be prompted for a version to open it in, selecting missing version or latest LTS version will work best.
    - wait for editor to load and enjoy.

gameplay notes :

the player can use the mouse to interact with the world, clicking on weapon parts for actions and dragging seals onto weapon parts to apply
effects. left click can be used to remove seals immediatly.

effects and resources : 
  - there were 3 resource pools with adrenaline, blood and psyche which were each divided into 2 humours, the sum of these humours would equal the
    fluid total and when one humour was changed the other would change as well creating a balance between them.
  - an effects system was implemented alongside the resources, effects could be applied which would trigger at certain points in the game loop such
    as on turn end or on attack. effects were simple, they could change your fluid total, rebalance your humours or would add/remove effects based
    on other effects.
  - some effects would trigger and remove themselves while others would stick around, of the latter the idea was that these passive effects could
    evolve/change based on what other effects are applied such as positive effects could become negative and vice versa if too many were applied.
  - the end goal of this system was that it could support a massive variety of potential effects which could interact and chain off each other
    which would allow a lot of depth within a relatively simplistic system.

actions, weapons and seals: 
  - players and enemies would have a limited pool of actions(3-6) which they could choose from during there turn, each action had a cost either
    rebalancing a humour or decreasing a fluid and in exchange they would apply effects to yourself or your enemy.
  - these actions were represented by a weapon, the player could wield 1 or 2 and each one would have 3 parts each corresponding to an action.
  - the plan was that out of combat the player would select weapons and apply seals to the various parts/actions. each seal had applied effects and
    a cost with the intention being that the player had to balance powerful high cost actions with weaker low cost actions.

enemies : 
  - development never progressed far enough for enemy AI to be developed, there was going to be a number of enemies, each with unique resource amounts
    and actions, those enemy actions would each have a priority that the ai would use to decide what to do.
  - in effect each enemy would have a pattern or gimmick it would default to such as applying X effect and using Y effect to turn X effect into a
    new, more beneficial Z effect.

please direct all inquiries, questions and problems to ruled.dev@gmail.com
