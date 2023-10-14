# KIT725 23S2 Assignment \#4 - Group 5

## Group 5

* Dongyi Guo - 662970
* (George) Thuan Pin Goh - 422333
* Ronghua Yang - 640602

## Important Guideline

* All newly generated code must be written within the provided code files.
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

All exploit code (as a client) must be added in the **Test.cs** Class and called from **Program.cs**

* **After the function implemented in Test.cs, call it in Program.cs.**

Each exploit must be coded in a separate method/function. The name of the method/function must include a CWE reference and the student name who implemented the exploit. For example, CWE-190 exploited by Don Yi, should be implemented as a function named CWE190_DonYi.

## CWE Listings - Top 25

* 787: Out of Bounds Write
* 125: Out of Bounds Read
* 190: Integer Overflow or Wraparound
* 22: Improper Limitation of a Pathname to a Restricted Directory
* 77: Improper Neutralization of Special Elements used in commands ("Command injection")
* 78: Improper Neutralization of Special Elements used in OS commands ("OS command injection")
* 79: Improper Neutralization of Input During Web Page Generation ("XSS")
* 89: Improper Neutralization of Special Elements used in an SQL Command ("SQL Injection")
* 94: Improper Control of Generation of Code ("Code Injection")
* 119: Improper Restriction of Operations within the Bounds of a Memory Buffer
* 169: Improper Privilege Management
* 276: Incorrect Default Permissions
* 287: Improper Authentication
* 863: Incorrect Authorization
* 798: Hard-coded Credentials
* 862: Missing Authorization
* 306: Missing Critical Function's Authorization
* 352: Cross-Site Request Forgery (CSRF)
* 918: Server-Side Request Forgery (SSRF)
* 416: Use After Free
* 434: Unrestricted Upload of File Type with Danger
* 476: Null Pointer Dereference 
* 502: Deserialization of Untrusted Data
* 362: Concurrent Execution using Shared Resource with Improper Synchronization ("Race Condition")

## CWE Listings - On the cusp

* 617: Reachable Assertion
* 427: Uncontrolled Search Path Element
* 611: Improper Restriction of XML External Entity Reference
* 770:  Allocation of Resources Without Limits of Throttling
* 200: Exposure of Sensitive Information to an Unauthorized Actor
* 732: Incorrect Permission Assignment for Critical Resource
* 601: URL Redirection to Untrusted Site ("Open Redirect")
* 1321: Improperly Controlled Modification of Object Prototype Attributes ("Prototype Pollution")
* 295: Improper Certificate Validation
* 522: Insufficiently Protected Credentials
* 401: Missing Release of Memory aftertion Effective Lifetime
* 400: Uncontrolled Resource Consumption
* 639: Authorization Bypass Though User-Controlled Key
* 59: Improper Link Resolution Before File Access ("Link Following")
* 668: Exposure of Resource to Wrong Sphere

