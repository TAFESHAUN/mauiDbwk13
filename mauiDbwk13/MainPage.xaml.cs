﻿
using mauiDbwk13.Data;
using mauiDbwk13.Views;
using System.Collections.ObjectModel;

namespace mauiDbwk13;

public partial class MainPage : ContentPage
{

    private Repository _repo;

    ObservableCollection<Products> productL = new ObservableCollection<Products>();
    public ObservableCollection<Products> ProductL { get { return productL; } }

    public MainPage()
	{

        InitializeComponent();

        //Load DB
        var repo = new Repository();
        var productList = repo.GetProduct();
        _repo = repo;

        ProductView.ItemsSource = productL;

        foreach (var product in productList)
        {
            productL.Add(product);
        }

    }

    async void ProductView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var product = e.SelectedItem as Products;
        await Navigation.PushAsync(new Details(product, _repo));
    }

    async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Insert(_repo));
    }
}

