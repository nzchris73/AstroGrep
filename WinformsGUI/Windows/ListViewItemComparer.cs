using System;
using System.Collections;
using System.Windows.Forms;

namespace AstroGrep.Windows
{
   /// <summary>
   /// Used for sorting of list view columns
   /// </summary>
   /// <remarks>
   ///   AstroGrep File Searching Utility. Written by Theodore L. Ward
   ///   Copyright (C) 2002 AstroComma Incorporated.
   ///   
   ///   This program is free software; you can redistribute it and/or
   ///   modify it under the terms of the GNU General Public License
   ///   as published by the Free Software Foundation; either version 2
   ///   of the License, or (at your option) any later version.
   ///   
   ///   This program is distributed in the hope that it will be useful,
   ///   but WITHOUT ANY WARRANTY; without even the implied warranty of
   ///   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   ///   GNU General Public License for more details.
   ///   
   ///   You should have received a copy of the GNU General Public License
   ///   along with this program; if not, write to the Free Software
   ///   Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
   /// 
   ///   The author may be contacted at:
   ///   ted@astrocomma.com or curtismbeard@gmail.com
   /// </remarks>
   /// <history>
   /// [Curtis_Beard]	   02/06/2005	Created
   /// [Curtis_Beard]	   07/07/2006	CHG: add support for count column sorting
   /// [Curtis_Beard]		02/17/2012	CHG: update listview sorting
   /// </history>
   internal class ListViewItemComparer : IComparer
   {
      private int col;
      private SortOrder order;

      /// <summary>
      /// Initializes a new instance of the ListViewItemComparer class.
      /// </summary>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// </history>
      public ListViewItemComparer()
      {
         col = 0;
         order = SortOrder.Ascending;
      }

      /// <summary>
      /// Initializes a new instance of the ListViewItemComparer class.
      /// </summary>
      /// <param name="column">Column to sort</param>
      /// <param name="sort">Sort Order</param>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// </history>
      public ListViewItemComparer(int column, SortOrder sort)
      {
         col = column;
         order = sort;
      }

      /// <summary>
      /// Handles the comparison of the current column of ListViewItems.
      /// </summary>
      /// <param name="x">Value 1</param>
      /// <param name="y">Value 2</param>
      /// <returns>The resultant comparison of the given values based on Sort Order.</returns>
      /// <history>
      /// [Curtis_Beard]		07/21/2006	Created
      /// [Curtis_Beard]		02/17/2012	CHG: update listview sorting
      /// [Curtis_Beard]		01/08/2019	CHG: 119, add line hit count
      /// </history>
      public int Compare(object x, object y)
      {
         //Implements System.Collections.IComparer.Compare

         int _returnVal;

         // Determine whether the type being compared is a date type.
         try
         {
            if (col == Constants.COLUMN_INDEX_COUNT)
            {
               // Parse the two objects passed as a parameter as a int.
               int firstInt = Core.Convertors.GetHitCountFromCountDisplay(((ListViewItem)x).SubItems[col].Text);
               int secondInt = Core.Convertors.GetHitCountFromCountDisplay(((ListViewItem)y).SubItems[col].Text);

               // Compare the two integers.
               if (firstInt < secondInt)
                  _returnVal = -1;
               else if (firstInt > secondInt)
                  _returnVal = 1;
               else
                  _returnVal = 0;
            }
            else if (col == Constants.COLUMN_INDEX_SIZE)
            {
               // Parse the two objects passed as a parameter as a long.
               long firstInt = Convert.ToInt64(((ListViewItem)x).SubItems[col].Tag);
               long secondInt = Convert.ToInt64(((ListViewItem)y).SubItems[col].Tag);

               // Compare the two integers.
               if (firstInt < secondInt)
                  _returnVal = -1;
               else if (firstInt > secondInt)
                  _returnVal = 1;
               else
                  _returnVal = 0;
            }
            else if (col == Constants.COLUMN_INDEX_DATE)
            {
               // Parse the two objects passed as a parameter as a DateTime.
               System.DateTime firstDate = DateTime.Parse(((ListViewItem)x).SubItems[col].Text);

               System.DateTime secondDate = DateTime.Parse(((ListViewItem)y).SubItems[col].Text);

               // Compare the two dates.
               _returnVal = DateTime.Compare(firstDate, secondDate);

               // If neither compared object has a valid date format, 
               // compare as a string.
            }
            else
            {
               // Compare the two items as a string.
               _returnVal = string.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }
         }
         catch
         {
            // Compare the two items as a string.
            _returnVal = string.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
         }

         // Determine whether the sort order is descending.
         if (order == SortOrder.Descending)
         {
            // Invert the value returned by String.Compare.
            _returnVal *= -1;
         }

         return _returnVal;
      }
   }
}