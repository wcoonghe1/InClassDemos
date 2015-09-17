<Query Kind="Program" />

void Main()
{
	//simple concatination expressions
	//"hello World"
	//5+7
	
	//simple C# sttements
	//string name = "Don";
	//string message = "hello " + name ;
	//message.Dump();
	
	//C# program
	//sub-rutine call
	SayHello("Amintha");
}

// Define other methods and classes here
public void SayHello(string name)
{
	string message = "hello " + name;
	message.Dump();
}