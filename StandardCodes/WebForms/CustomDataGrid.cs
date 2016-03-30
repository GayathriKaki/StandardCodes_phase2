// Created on        Created By
// JULY 2010      Gayathri (16727)

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Data;

namespace StandardCodes
{
    public class CustomDataGrid : DataGrid
    {
        public static int intCopy = 0;
        static CustomDataGrid()
        {
            CommandManager.RegisterClassCommandBinding(
            typeof(CustomDataGrid),
            new CommandBinding(ApplicationCommands.Paste,
            new ExecutedRoutedEventHandler(OnExecutedPaste),
            new CanExecuteRoutedEventHandler(OnCanExecutePaste)));
        }

        #region Clipboard Paste

        private static void OnCanExecutePaste(object target, CanExecuteRoutedEventArgs args)
        {
            ((CustomDataGrid)target).OnCanExecutePaste(args);
        }

        /// <summary>
        /// This virtual method is called when ApplicationCommands.Paste command query its state.
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnCanExecutePaste(CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = CurrentCell != null;
            args.Handled = true;
        }

        private static void OnExecutedPaste(object target, ExecutedRoutedEventArgs args)
        {
            ((CustomDataGrid)target).OnExecutedPaste(args);
        }

        /// <summary>
        /// This virtual method is called when ApplicationCommands.Paste command is executed.
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnExecutedPaste(ExecutedRoutedEventArgs args)
        {
            try{
            Debug.WriteLine("OnExecutedPaste begin");

            // parse the clipboard data            
            List<string[]> rowData = ClipboardHelper.ParseClipboardData();
            intCopy = 1;
            bool hasAddedNewRow = false;

            // call OnPastingCellClipboardContent for each cell
            int minRowIndex = Items.IndexOf(CurrentItem);
            int maxRowIndex = Items.Count - 1;
            int minColumnDisplayIndex = (SelectionUnit != DataGridSelectionUnit.FullRow) ? Columns.IndexOf(CurrentColumn) : 0;
            int maxColumnDisplayIndex = Columns.Count ;
            int rowDataIndex = 0;
            for (int i = minRowIndex; i <= maxRowIndex && rowDataIndex < rowData.Count-1; i++, rowDataIndex++)
            {
                if (CanUserPasteToNewRows && CanUserAddRows && i == maxRowIndex)
                {
                    // add a new row to be pasted to
                    ICollectionView cv = CollectionViewSource.GetDefaultView(Items);
                    IEditableCollectionView iecv = cv as IEditableCollectionView;
                    if (iecv != null)
                    {
                        hasAddedNewRow = true;
                        iecv.AddNew();

                        if (rowDataIndex + 1 < rowData.Count)
                        {
                            // still has more items to paste, update the maxRowIndex
                            maxRowIndex = Items.Count - 1;
                        }
                    }
                }
                else if (i == maxRowIndex)
                {
                    continue;
                }

                int columnDataIndex = 0;
                for (int j = minColumnDisplayIndex; j < maxColumnDisplayIndex && columnDataIndex < rowData[rowDataIndex].Length; j++, columnDataIndex++)
                {

                    try
                    {
                        DataGridColumn column = ColumnFromDisplayIndex(j);
                        column.OnPastingCellClipboardContent(Items[i], rowData[rowDataIndex][columnDataIndex]);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("The columns in the grid dosn't match with the copied cells.");
                    }

                }
            }

            // update selection
                if (hasAddedNewRow)
                {
                    UnselectAll();
                    UnselectAllCells();

                    CurrentItem = Items[minRowIndex];

                    if (SelectionUnit == DataGridSelectionUnit.FullRow)
                    {
                        SelectedItem = Items[minRowIndex];
                    }
                    else if (SelectionUnit == DataGridSelectionUnit.CellOrRowHeader ||
                             SelectionUnit == DataGridSelectionUnit.Cell)
                    {
                        SelectedCells.Add(new DataGridCellInfo(Items[minRowIndex], Columns[minColumnDisplayIndex]));
                        
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("The columns in the grid dosn't match with the copied cells.");
            }
        }

        /// <summary>
        ///     Whether the end-user can add new rows to the ItemsSource.
        /// </summary>
        public bool CanUserPasteToNewRows
        {
            get { return (bool)GetValue(CanUserPasteToNewRowsProperty); }
            set { SetValue(CanUserPasteToNewRowsProperty, value); }
        }

        /// <summary>
        ///     DependencyProperty for CanUserAddRows.
        /// </summary>
        public static readonly DependencyProperty CanUserPasteToNewRowsProperty =
            DependencyProperty.Register("CanUserPasteToNewRows",
                                        typeof(bool), typeof(CustomDataGrid), 
                                        new FrameworkPropertyMetadata(true, null, null));
        
        #endregion Clipboard Paste
    }
}
