using mauiDbwk13.Data;

namespace mauiDbwk13.Views;

public partial class Insert : ContentPage
{
    private Repository _repo;
    private Products productInsert;
    public Insert(Repository repo)
    {
        InitializeComponent();
        _repo = repo;
        var prod = new Products();
        productInsert = prod;
    }

    //Insert
    async void Button_Clicked(object sender, EventArgs e)
    {

        productInsert.Product = NameInsert.Text;
        productInsert.Price = Convert.ToInt32(PriceInsert.Text);
        productInsert.Code = CodeInsert.Text;

        try
        {
            _repo.InsertProduct(productInsert);
            //MEMORY BOG -> Thousands of pages
            await Navigation.PushAsync(new MainPage());//PASS THE REPO BUT PROPER MVVM -> BACK BTN
            //await Navigation.PopAsync();
            //POP AND REFRESH OF MY FIRST MAIN PAGE.
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}