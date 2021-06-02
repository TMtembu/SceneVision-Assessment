using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    /// <summary>
    /// Global variable. which shall be used across the application
    /// </summary>
    double _results;
    string _operator = string.Empty;
    bool _isValueEntered = false;
    bool _isEqualEntered = false;
    string _firstValue;
    string _secondValue;
    double _memoryValue;
    bool _isMemorySaved = false;
    string initialScreenDisplay = "0";
    //------------------------------------------------------------------------------------------
    public MainWindow()
    {
      InitializeComponent();
      lblDisplayOperation.HorizontalContentAlignment = HorizontalAlignment.Right;
      btn_ClearHistory.Visibility = Visibility.Hidden;
      btn_MC.IsEnabled = false;
      btn_MR.IsEnabled = false;
      lblHistoryActive.Visibility = Visibility.Visible;
      lblMemoryActive.Visibility = Visibility.Hidden;
      richTextBox_History.Visibility = Visibility.Visible;
      richTextBox_memory.Visibility = Visibility.Hidden;
    }

    ////------------------------------------------------------------------------------------------
    //private void txt_input_TextChanged(object sender, TextChangedEventArgs e)
    //{

    //}

    //------------------------------------------------------------------------------------------
    private void operatorButton_OnClickEvent(object sender, RoutedEventArgs e)
    {
      Button operatorButton = (Button)sender;

      if (_results != 0)
      {
        _isValueEntered = true;
        _results = double.Parse(txt_input.Text);
        _operator = (string)operatorButton.Content;
        lblDisplayOperation.Content = Convert.ToString(_results) + " " + _operator;
        txt_input.Text = string.Empty;
      }
      else
      {
        _operator = (string)operatorButton.Content;
        _results = double.Parse(txt_input.Text);
        txt_input.Text = string.Empty;
        lblDisplayOperation.Content = Convert.ToString(_results) + " " + _operator;
      }
      _firstValue = (string)lblDisplayOperation.Content;
    }

    //------------------------------------------------------------------------------------------
    private void valuesButton_OnClickEvent(object sender, RoutedEventArgs e)
    {
      Button valueButton = (Button)sender;
      if (_isEqualEntered && (lblDisplayOperation.Content.ToString().Contains("=")))
      {
        lblDisplayOperation.Content = string.Empty;
        txt_input.Text = initialScreenDisplay;
      }
      if ((txt_input.Text == initialScreenDisplay) || _isValueEntered)
      {
        txt_input.Text = string.Empty;
      }
      _isValueEntered = false;
      if ((string)valueButton.Content == ".")
      {
        if (!txt_input.Text.Contains("."))
        {
          txt_input.Text = txt_input.Text + (string)valueButton.Content;
        }
      }
      else
      {
        txt_input.Text = txt_input.Text + (string)valueButton.Content;
      }
    }
    //------------------------------------------------------------------------------------------
    private void btn_Equal_Click(object sender, RoutedEventArgs e)
    {
      Button _sender = (Button)sender;
      _isEqualEntered = true;
      _secondValue = txt_input.Text;

      lblDisplayOperation.Content += txt_input.Text + " =";
      switch (_operator)
      {
        case "+":
          txt_input.Text = (_results + double.Parse(txt_input.Text)).ToString();
          break;
        case "-":
          txt_input.Text = (_results - double.Parse(txt_input.Text)).ToString();
          break;
        case "÷":
          txt_input.Text = (_results / double.Parse(txt_input.Text)).ToString();
          break;
        case "x":
          txt_input.Text = (_results * double.Parse(txt_input.Text)).ToString();
          break;
        default:
          break;
      }
      _results = double.Parse(txt_input.Text);
      _operator = string.Empty;
      btn_ClearHistory.Visibility = Visibility.Visible;
      richTextBox_History.AppendText("\t\t" + lblDisplayOperation.Content + " " + txt_input.Text + "\n");
      lblComment.Content = string.Empty;

    }

    private void btn_Clear_Click(object sender, RoutedEventArgs e)
    {
      lblDisplayOperation.Content = string.Empty;
      txt_input.Text = initialScreenDisplay;
      _results = 0;
    }

    private void btn_RemoveChar_Click(object sender, RoutedEventArgs e)
    {
      if (txt_input.Text.Length > 0)
      {
        txt_input.Text = txt_input.Text.Remove(txt_input.Text.Length - 1, 1);
      }
      else if (txt_input.Text == string.Empty)
      {
        txt_input.Text = initialScreenDisplay;
      }

    }

    //---------------------------------------------------------------------------------------------------------
    private void Button_Click_1(object sender, RoutedEventArgs e)
    {

      if (lblHistoryActive.Visibility.Equals(Visibility.Visible) && richTextBox_History.Document.Blocks.Count > 1)
      {
        richTextBox_History.Document.Blocks.Clear();
        lblComment.Content = "There's no history yet";
      }
      else if (lblMemoryActive.Visibility.Equals(Visibility.Visible) && richTextBox_memory.Document.Blocks.Count > 1)
      {
        richTextBox_memory.Document.Blocks.Clear();
        lblComment.Content = "There's nothing saved in memory";
        btn_MC.IsEnabled = false;
        btn_MR.IsEnabled = false;
      }
      btn_ClearHistory.Visibility = Visibility.Hidden;
      richTextBox_History.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
    }

    //---------------------------------------------------------------------------------------------------------
    private void btn_ClearInput_Click(object sender, RoutedEventArgs e)
    {
      txt_input.Text = initialScreenDisplay;
    }

    //---------------------------------------------------------------------------------------------------------
    private void buttonMemorySave_OnClickEvent(object sender, RoutedEventArgs e)
    {
      _memoryValue = double.Parse(txt_input.Text);
      btn_MC.IsEnabled = true;
      btn_MR.IsEnabled = true;
      _isMemorySaved = true;
      btn_ClearHistory.Visibility = Visibility.Visible;
      richTextBox_memory.AppendText("\t\t" + _memoryValue + "\n");
      lblComment.Content = string.Empty;
    }

    //---------------------------------------------------------------------------------------------------------
    private void buttonMemoryRecall_OnClickEvent(object sender, RoutedEventArgs e)
    {
      txt_input.Text = _memoryValue.ToString();
    }

    //---------------------------------------------------------------------------------------------------------
    private void buttonMemoryClear_OnClickEvent(object sender, RoutedEventArgs e)
    {
      txt_input.Text = initialScreenDisplay;
      _memoryValue = 0;
      btn_MC.IsEnabled = false;
      btn_MR.IsEnabled = false;
      btn_ClearHistory.Visibility = Visibility.Hidden;
      richTextBox_memory.Document.Blocks.Clear();
      lblComment.Content = "There's nothing saved in memory";

    }

    //---------------------------------------------------------------------------------------------------------
    private void buttonMemoryAdd_OnClickEvent(object sender, RoutedEventArgs e)
    {
      _memoryValue = _memoryValue + double.Parse(txt_input.Text);
      txt_input.Text = _memoryValue.ToString();

    }

    //---------------------------------------------------------------------------------------------------------
    private void buttonSubtractMemory_OnClickEvent(object sender, RoutedEventArgs e)
    {
      _memoryValue = _memoryValue - double.Parse(txt_input.Text);
      txt_input.Text = _memoryValue.ToString();
    }

    //---------------------------------------------------------------------------------------------------------
    private void btn_History_Click(object sender, RoutedEventArgs e)
    {
      if (richTextBox_History.Document.Blocks.Count <= 1)
      {
        lblComment.Content = "There's no history yet";
        btn_ClearHistory.Visibility = Visibility.Hidden;
      }
      else
      {
        lblComment.Content = string.Empty;
        btn_ClearHistory.Visibility = Visibility.Visible;
      }
      richTextBox_memory.Visibility = Visibility.Hidden;
      richTextBox_History.Visibility = Visibility.Visible;
      lblHistoryActive.Visibility = Visibility.Visible;
      lblMemoryActive.Visibility = Visibility.Hidden;
    }
    //---------------------------------------------------------------------------------------------------------
    private void btn_Memory_Click(object sender, RoutedEventArgs e)
    {
      if (richTextBox_memory.Document.Blocks.Count <= 1)
      {
        lblComment.Content = "There's nothing saved in memory";
        btn_ClearHistory.Visibility = Visibility.Hidden;
      }
      else
      {
        lblComment.Content = string.Empty;
        btn_ClearHistory.Visibility = Visibility.Visible;
      }
      richTextBox_History.Visibility = Visibility.Hidden;
      richTextBox_memory.Visibility = Visibility.Visible;
      lblHistoryActive.Visibility = Visibility.Hidden;
      lblMemoryActive.Visibility = Visibility.Visible;
    }
  }
}
