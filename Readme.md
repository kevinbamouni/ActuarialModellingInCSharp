// This constructor has no parameters. The parameterless constructor
// is invoked in the processing of object initializers.
// You can test this by changing the access modifier from public to
// private. The declarations in Main that use object initializers will
// fail.
public StudentName() { }

// The following constructor has parameters for two of the three
// properties.
public StudentName(string first, string last)
{
    FirstName = first;
    LastName = last;
}