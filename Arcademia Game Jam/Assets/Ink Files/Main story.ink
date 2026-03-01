VAR pride_done = false
VAR famine_done = false
VAR death_done = false

-> pandora_home

=== pandora_home ===

#bg 
#speaker: Hope

"Hey."

"So... not to alarm you or anything."

"But you may have accidentally released every evil in existence"

#speaker: Pandora 

"First of all..."

"ACCIDENTALLY is such a strong word."



#speaker: Hope

"... I literally saw you unseal the box."

"I was there when the God's said don't open it."

#speaker: Pandora

"... Hehe"


#speaker: Hope

" ... "

" You really are... truly unbelievable."

#speaker: Pandora 

"Also, not to be rude but who... or what are you?"

#speaker: Hope

"I'm an apparation, the same as the others that escaped."

"And i'm the apparation of Hope."

"This is actually... really really bad."

#speaker: Pandora 

"Don't even worry about it."

"I can just seal it again"

#speaker: Hope

" You do know that's not how it works right?"


#speaker: Pandora 

"You could say... I have Hope."

#speaker: Hope

" I'm gonna... ignore that."


#speaker: Pandora 

"Well that's my plan, i'll make it work...somehow."

#speaker: Hope

" ... we may be able to help each other."

#speaker : Pandora 

" See! I told you it'll work itself out eventually... balance and stuff."

-> explanation_system 

=== explanation_system  ===

#bg

#speaker: Pandora

"But like let's wait a minute."

" I have a LOT of questions."

#speaker: Hope

" I would assume so."

" You did just release ancient evils from all 3 realms."

#speaker: Pandora 

"Right, we can start there with all this realm stuff."

#speaker: Hope

" So we have 3 realms to this world"

" Firstly, Ouranos (Sky) - where the God's and godly virtues reside, I think you humans refer to it as Mount Olympus in this era."

" Secondly, Gaia (Earth) - this realm is where you humans reside."

"Lastly, we have Erebos (Underworld) - where souls that have passed return to."

#speaker: Pandora

" ... You could have just said from each cosmos."

" But anyway, to get them back in the box do we just stuff them back in or?"

#speaker: Hope

"Well, we could have if... Pandora's Box didn't manifest as an apparation."

#speaker: Pandora 

"But the box is literally right here?"

#speaker: Hope

"That is just a box, the essence of Pandora's box has manifested as an apparation just like the others have."

#speaker: Pandora

"Great."

"So what do we do?"

#speaker: Hope

"We need to defeat them in battle."

#speaker: Pandora 

"Why and how exactly???"

#speaker: Hope

" Because they're apparations."

" The moment the box opened, they partially manifested into this world."

" They have gained a form, and power."

" In order to return them to the box, we need to break their ties to this current world."

#speaker: Pandora

"In simpler terms?"

#speaker: Hope

" Use my power, put your soul on the line and ... beat them in a fight."

#speaker: Pandora

"My WHAT? why am I sacrificing my soul exactly?"

"Can't we just use your power?"

#speaker: Hope

" I'm not manifested as they are, my essence is enclosed in the box."

" The only way I could interact with them is through you, and apparations bound to humans are not fully manifested either."

#speaker: Pandora 

" Hold on, what exactly does binding entail?"

" And i'm still unclear on how my SOUL is the price?"

#speaker: Hope

" Spirits can only be bound to a soul."

" Balance still applies, since we cannot take harm our contractor does."

" And contractor is what you will be, a binding is a form of a contract."

" You stake your soul to use our power."

#speaker: Pandora 

" Right."

" So basically we duo this?"

#speaker: Hope

"Well... once you defeat a spirit you need to bind them to you to."

" We need all of them to defeat the essence of Pandora's Box."

#speaker: Pandora 

"Well we might as well get to it, i'll ask whatever else when I need to."

" Doesn't seem like I have of a much choice here anyway."

#speaker: Hope

" You learn fast."

" Let's get to it"

#speaker: Hope

" Let's go after the three worst, and most dangerous apparations."

" They've all chosen to resided in a realm."

#speaker: Pandora 

"Great! My first time ever sight seeing these areas and all it took was death looming over my head!"

#speaker: Hope

" If we defeat them, we turn their power into ours."

" Worth the risk if you ask me."

-> choose_path

=== choose_path ===

#bg 

#speaker: Pandora 

" Where do we go from here?"

# speaker: Hope

" It's up to you, any of the realms we haven't gone to yet will do."

" So where do you want to go?"

* {pride_done == false} Ouranos (Mount Olympus) -> pride_scene
* {famine_done == false} Gaia (Earth) -> famine_scene
* {death_done == false} Erebos (Underworld) -> death_scene 
*{pride_done && famine_done && death_done} Pandora's Box -> final_area

=== pride_scene ===

#bg 

#speaker: Pandora 

" So tell me about this Ouranos apparation."

#speaker: Hope

" Ugh, they're insufferable to be around."

" Like truly, insufferable."

#speaker: Pandora

" I meant like, what they are? What powers do they have..."

#speaker: Hope

"*cough*"

" I knew that of course."

" They're the apparation of Pride."

" We're close, you can... meet them for yourself."

" They have particularly strong healing and defense, basic attack though."

#speaker: Pandora 

" Alright seems... easy enough."

#speaker: Hope

" Get ready, they're here.

#bg 

#speaker: Pride

" You stand before perfection."

" Kneel."

#speaker: Pandora 

"..."
" I see what you mean now."

#speaker: Pride

" It is the law of nature to bow before greatness."

#speaker: Pandora 

" I don't even listen to the God's."

" What makes you think I'm bowing down to you?"

#speaker: Hope

" You two are really... something else."

#battle: pride

-> pride_after

=== pride_after ===

#bg
#speaker: Pride 

"Impossible..."

"How could this happen?"

#speaker: Pandora 
" No, no."

" It's very possible, I just literally defeated you."

#speaker: Hope

~ pride_done = true

#speaker: Hope

" Nice."

" One down."

" I like the specialty you chose for them."

" Let's hope it was the better choice for the battle to come."

-> choose_path

=== famine_scene ===

#bg

#speaker: Pandora 

" Okay cool, Gaia - basically my playground."

#speaker: Hope

" Pfft."

" Child, you do not even KNOW half of the wonders of Gaia."

#speaker: Pandora 

"But like I've been here for my whole life."

"Surely I have an advantage over an apparation that got here just today."

#speaker: Hope

"... Apparations like us aren't bound by time."

" Sigh."

" Just listen, the apparation you're up against is Famine."

#speaker: Pandora 

" So just like... being hungry?"

" What do they do? Steal the bread from my pocket?"

#speaker: Hope

"... No."

" Just no."

" Their specialty is healing and attack, their defense is very weak."

" i can sense them, prepare yourself."

#bg

#speaker: Famine

"Hungry..."

"So... hungry."

" Everything needs to starve like I am."

#speaker: Pandora 

"Absolutely not, do you know how hard I worked for food??"

" I mean seriously have you SEEN this economy?"

#speaker: Hope

"That's Famine."

#speaker: Pandora 

" I figured."

" Let's do this."

#battle: famine 

-> famine_after

=== famine_after ===

#bg 

#speaker: Famine

"The hunger..."

"... Is fading?"

#speaker: Hope

"Wow i'm surprised you actually did it."

#speaker: Pandora 

"Please, that was so easy."

#speaker: Hope

" I'm glad you're getting the hang of things."

" Let's continue our adventure then."

-> choose_path

=== death_scene ===

#bg
#speaker: Pandora 

" So this is fun and all -"

" But do we really have to go here?"

"Surely, the other two will be enough."

#speaker: Hope

"Stop being a scardey cat."

" What about this place makes you so afraid"

" It's just a river and a boat."

#speaker: Pandora

"... In a creepy cave"

" Going to the places human are after they DIE?"

" It's giving bad vibes"

#speaker: Hope

"... I'm here with you."

" Nothing will happen to you."

" You have my word."

#speaker: Pandora 

"Awww you love me"

#speaker: Hope

" Don't make me regret saying it."

" Let's focus on the apparation ahead of us...Death."

#speaker: Pandora

" You're kidding... right?"

#speaker: Hope

" No, they're here."

" Listen quickly, they're very strong at attacking and defending."

" Very bad at healing."

#bg

#speaker: Death
" All things return to me."

" Your soul shall by claimed by me too."

#speaker: Pandora 

" Nope. No."

"They're too edgy, even for me."

#speaker: Hope

"That's Death."

#speaker: Pandora

" Yeah, I got that."

" They gives me the ick, let's finish this quickly."

#battle: death 

-> death_after

=== death_after ===

#bg
#speaker: Death

" Interesting. "

" You escaped Deaths grasp."

#speaker: Pandora 

" Wait can I get this framed or something."

" I mean this is total bragging rights."

#speaker: Hope

" Why would you want a souvenier of that?"

" Such a curious human."

#speaker: Pandora 

" Onward! We are on a roll."

#speaker: Hope

" Very well, but pace youself."

-> choose_path

=== final_area ===

#bg 

#speaker: Hope
"We're finally here."

" The final battle."

#speaker: Pandora 

" We, mainly I, captured the all the spirits."

#speaker: Hope

" Which was made possible with MY power."

#speaker: Pandora

" Yeah, yeah."

" Who cares about specifics anyway."

" Let's whoop Pandora's box!"

#speaker: Hope

" Remember, the box itself is still open."

" It can utilise all the typings since it is not bound to a single realm."

#speaker: Pandora

" Great so like, It's a coin toss?"

" Which version of it we fight it just dependant on it's mood?"

#speaker: Hope

" No..."

" If only it were that simple."

 // " It switches typings randomly."
 
 #speaker: Pandora 
 
 " Oh great a good old, overpowered fight'
 
 " Bleak chances, blah blah."
 
 " Just like the hero stories."
 
 #speaker: Hope
 
 " Well then, if we defeat it - a hero you will be."
 
 #speaker: Pandora 
 
 " Okay NOW, you're talking my language."
 
 " Let's do this."
 
 #battle: box
 
 -> final_after 
 
 === final_after ===
 
 #bg
 
 #Speaker: Hope
 
 " Holy Realms."
 
 " We did it."
 
 #speaker: Pandora 
 " Told you it'd work out."
 
 #speaker: Hope
 
 " I don't mind you being cocky right now."
 
 " It feels surprisingly nice."
 
 #speaker: Pandora
 
 " So now? We just go our separate ways or?"
 
 #speaker Hope
 " No, this is only the beginning."
 
 #speaker: Pandora
 
 "..."
 
 " What?"
 
 #speaker: Hope
 
 " Surely you didn't think there was only 3 apparations?"
 
 " I did tell you that you released EVERY evil in existence."
 
 " I did not jest."
 
 #speaker: Pandora
 
 "..."
 
 "Well there goes my plans for the next eternity."
 
 -> DONE


