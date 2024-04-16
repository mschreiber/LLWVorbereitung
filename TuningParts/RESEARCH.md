# Research document LLW

# Project structure
``` bash
# LibraryManagement.Models
#  -> Attributes
#     ISBNAttribute.cs
#   BaseModel.cs
#   Book.cs

# LibraryManagement.Access
#  -> Interfaces
#     IDataAccess.cs
#  -> Extensions
#     BookExtension.cs
#     ValidationExtension.cs
#  ValidateBookDataAccess.cs (Abstract class for validation)

# LibraryManagement.Access.Memory
#  BookDataAccess.cs (Inherits from ValidateBookDataAccess)

# LibraryManagement.Business
# BookDataService.cs (service between UI and backend)
```

## DataGridView

``` csharp
// FormDesigner
dataGridViewData.MultiSelect = false;
dataGridViewData.ReadOnly = true;
dataGridViewData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

dataGridViewData.SelectionChanged += dataGridViewData_SelectionChanged;
```

``` csharp
// Form
dataGridViewData.DataSource = null;
dataGridViewData.DataSource = _bookDataService.Books;

dataGridViewData.Columns["Property1"].DisplayIndex = 0;
dataGridViewData.Columns["Property2"].DisplayIndex = 1;
dataGridViewData.Columns["Property2"].HeaderText = "Cell Name";

dataGridViewData.AutoResizeColumns();
```

``` csharp
// Form
private void dataGridViewData_SelectionChanged(object sender, EventArgs e)
{
    _variable = null;

    try
    {
        _variable = (sender as DataGridView)?.CurrentRow?.DataBoundItem as Book;
    }
    catch { }
}
```

## Validator

``` csharp
public static class ValidationExtension
{
    public static T Validate<T>(this T item)
    {
        ValidationContext context = new(item.IsNotNull());
        List<ValidationResult> results = new();

        if (!Validator.TryValidateObject(item, context, results, true))
        {
            foreach (ValidationResult result in results)
            {
                throw new ArgumentException(result.ErrorMessage, result.MemberNames?.FirstOrDefault());
            }
        }
        return item;
    }

    public static T IsNotNull<T>(this T book)
    {
        _ = book ?? throw new NullReferenceException(nameof(T));

        return book;
    }
}
```

## Attributes

``` csharp
// Model
public class Book : BaseModel
{
    [Required]
    [StringLength(13, MinimumLength = 13)]
    [ISBN]
    public string ISBN { get; set; }

    // ...
}
```

``` csharp
// ISBNAttribute.cs
public class ISBNAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        string isbn = value as string;

        // ISBN Nummer prüfen!

        if (string.IsNullOrWhiteSpace(isbn))
            return false;

        return true;
    }
}
```

## Generic Interface

``` csharp
// IDataAccess.cs
public interface IDataAccess<T> : IDisposable
{
    void Create(T item);
    void Delete(int id);
    void Update(int id, T item);
    List<T> Read();
}
```

## Abstract Validator

``` csharp
public abstract class ValidateBookDataAccess : IDataAccess<Book>
{
    public virtual void Create(Book item)
    {
        item.RemoveCharsFromISBN()
            .Validate()
            .Duplicate(Read());
    }

    public virtual void Update(int id, Book item)
    {
        if (!item.RemoveCharsFromISBN()
                    .Validate()
                    .IdExists(Read()))
            throw new Exception(nameof(Update));
    }

    public virtual void Delete(int id)
    {
        if (!id.IdExists(Read()))
            throw new Exception(nameof(Delete));
    }

    public abstract List<Book> Read();

    public abstract void Dispose();
}
```

