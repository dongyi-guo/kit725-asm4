# KIT725 23S2 Assignment \#4 - Group 5

## Group 5

* Dongyi Guo - 662970
* (George) Thuan Pin Goh - 422333
* Ronghua Yang - 640602

## Important Guideline

* All newly generated code must be written
within the provided code files.
* Additional classes must not be added to the source code.
* The names of the classes and methods/functions must not change.
* New code/updated code must be written within functions inside a class.
* Class headers and namespaces must not be removed or changed.
* Exploit and test code must be added Test.cs.
* Disclaimer statements must not be removed from any class file.

## Patching the Code Weaknesses

The provided code has 25+ weaknesses relevant to 18 unique CWEs from the Top-25 and On the Cusp listings of 2023. The following example must be followed as a “template of code comments” that must be added before each method/function where weakness is identified, exploited and patched.

Original Code:

```csharp
public string TestCodeWithVulnerabilities(int someVar)
{
	Console.WriteLine("This code has a vulnerability");
}
```

Code after being modified:

```csharp
/*
* 1. ← A single piece of code may have more than 1 weakness use the number to identify them individually
* Identified as CWE-123
* Date - Identified by Name of the student < add this comment once the weakness is identified
* Date - Exploited by Name of the student < add this comment once the weakness is exploited
* Date - Patched and tested by Name of the student < add this comment once the weakness is patched and tested
*/
public string TestCodeWithVulnerabilities(int someVar)
{
	Console.WriteLine("The vulnerability in this code is now patched");
}
```

## Writing Exploit Code

All exploit code (as a client) must be added in the Test.cs Class and called from Program.cs

Each exploit must be coded in a separate method/function. The name of the method/function must include a CWE reference and the student name who implemented the exploit. For example, CWE-190 exploited by John Doe, should be implemented as a function named CWE190_JohnDoe.
