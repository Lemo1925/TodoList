using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaTodoListApp.Services;
using CommunityToolkit.Mvvm.Messaging;

namespace AvaloniaTodoListApp;

public partial class EditWindow : Window
{
    public EditWindow()
    {
        InitializeComponent();
        WeakReferenceMessenger.Default.Register<CloseWindowMessage>(this, (_, m)=> { this.Close(); });
    }
}