# Tic Tac Toe
Simple Tic Tac Toe Game that learns to play.

You can play at: https://repl.it/@ericksong/TicTacToe

I have heard of companies giving a Tic Tac Toe game as a project for potential applicants. As an interviewer, I wanted to assess how well it demonstrated my knowledge and see how long it would take me do to a quick and dirty implementation.

## My Requirements:
1. A console app of standard Tic Tac Toe
2. .NET Core
3. Computer had to make smart moves
4. Computer needs to learn from mistakes
5. Computer should play itself to get smart

## I wanted to answer a few questions:
1. Can I do it?
2. Is this a reasonable assignment just in terms of effort required?
3. What does this assess?
4. Learn stuff

### Can I do it?
I guess the answer is yes. I didn't do unit testing (see below). Does it work correctly? Maybe... That is a non-trivial question, lots of cases. I thought it played a decent game. I actually saw it using a strategy that I had never considered. (Trying to trap the player using the side middle squares.) It is easy to poison the cache by throwing games. However, it should eventually recover from this by factoring in lost games.

### Reasonableness
It took me 2.5 hours to complete the code to the initial check in. That seems decent since most of the examples I have seen don't implement any type of learning which was by far the most time-consuming part, probably 2 hours. However, most applicants would probably do something more than a console app which would probably add another couple of hours to make it pretty. Also, doing unit testing would also add a couple of hours. So, this is about an 8-hour project to do 'right'

### Skill Assessment
This did force me to do several things:

1. General coding abilities and style
2. Creating classes and separation of responsibilities
3. Patterns for learning algorithms
4. Basic User Experience understanding
5. Looping and logic structures
6. Storing and looking up data

### Stuff Learned
1. This is a tricky problem with edge cases, non-trivial, a bit humbling
2. I couldn't find a way to just have the machine learn with no knowledge of how to block and win. 
3. Having two random players play against each other just seems to produce more randomness. Maybe this is a factor of not playing enough games and needing to have better logic around eliminating bad games. It could also be due to how I had initial implemented the board matching and it could subsequently work.
4. I enjoy solving the hard/interesting parts of problems and not the ones that I have done before.

Perfect, no. Decent, yes.

I would not consider this ideal code, just one way to implement. There was little to no rework of the initial check in.

#### Note on Testing
I did this for fun and was more interested in the logic than the unit testing. I figure adding unit testing would have double the effort because unit testing the learning code would have been a big hassle and I didn't consider that fun. It would be great if someone else who really enjoys writing unit tests wanted to add that. Maybe another learning program could be written to play against this one to test it (them both) thoroughly.
