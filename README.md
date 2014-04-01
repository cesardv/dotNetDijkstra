dotNetDijkstra
==============

Attempt at a C# Dijkstra's Algorithm Implementation

### Requirements (comment while submitting in dropbox):
1. IDE/OS (Example: Java+Netbeans)
2. % completeness from your point of view (tell us exactly what your program can / cannot do)
3. Program should be your own work, as always. Citations are a must, if you looked anywhere for help!!!
4. Submit the WHOLE project (if we'll need to create projects for example, in Netbeans, for the whole 60 students on class... well, we'll get a headache and you'll get points off!)

Implement DIJKSTRA's algorithm for Single Source Shortest Path Problem with Binary Heaps. The running time should be O(ElogV)

### SUBMISSION INSTRUCTIONS:
1. submit here on D2L dropbox folder, on due date;
2. if problems with D2L: submit the link by email cc-ing all instructors (see instructions) to the zip-file with the source code in C/C++/JAVA (JDK)/Python/any other programming language and executable on PC (or e-mail zip).

Your program should read data from file (see samples below) and output a single number = sum of lengths of the shortest paths from node 0 to each node.

### INPUT FORMAT:
* The first line of each file below contains the number of vertices and the number of edges in the graph (in the format "n=XXXX m=XXXXX"). 
* The rest of the file is organized as follows:
    * each vertex i appears on a line by itself, followed by a line for each neighbor j>i of i (containing j and the length of edge (i,j)).
    * Each list of neighbors is ended by an empty line. Vertices i which do not have neighbors with an index greater than i are not represented.
	NOTE: Vertices are indexed from 0 to n-1.
	NOTE: each edge is mentioned only once with its smaller number endpoint

### SAMPLE INPUT:
* Using Given 1000.txt and 25000.txt. The length of the shortest path tree should be 10721073 and 625349 respectively.
*Program should give output in 3-10 seconds for the first input and less than 1 second for the second.

Given outputs are 100% correct.
If you happen to get different ones, it means that your program fails to check for all possible cases.
Note that it is harder to get the correct output for the "first_input" (bigger one). You have to think what could be the reason, and how to overcome it.
