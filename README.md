2/4/2025
Sensor Tower Take Home Assessment Write Up
Tech Stack:
•	C#
•	.Net Console App 
•	Visual Studio for IDE 
Where it Shines
•	Follows best practices. I implemented dependency injection. Which allows for loose coupling, easier unit testing, maintainability, code reusability, and readability.
•	Implemented unit testing.
•	Commented thoroughly. 
Where it falls flat
•	Can’t add file, the file location is hard coded.
•	No GUI, and output is printed in the console.
•	Performance is very slow. On average it takes 48 seconds which is insanely slow. 
•	The output for matching duplicates could be better.
•	It is not packaged into an executable so you need to open the project in Visual Studio.
Performance
Overall slow and with more time to develop I would work on improving the performance. I made the mistake of nesting loops which did not help. Takes an average of 48 seconds to finish executing with the file provided. 
Dependencies 
•	FuzzySharp: used to compare strings to see how similar they are.
•	xUnit for unit testing 
What I would improve given more time
•	Add a GUI that allows the user to attach a file, display output in GUI, and export output of grouped similar names to a file.
•	Improve the performance.
•	Improve the outputs for matching duplicates. 
•	I would have packaged the code into an executable. 
