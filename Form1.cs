using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace winFrm_SortLab
{
    public partial class Form1 : Form
    {
        #region drawing resource

        static private Pen _pen = Pens.Cyan;
        static private Graphics _g = null;
        static private int _sleepTimespan = 2;

        #endregion

        #region tools

        /// <summary>
        /// sub-sequence
        /// </summary>
        static protected Graphics SubPreapreSorting(Panel thePanel)
        {
            //# 設定座標系統
            //Graphics g = e.Graphics;
            Graphics g = thePanel.CreateGraphics();
            g.TranslateTransform(30, thePanel.Height - 30); // 變更原點
            g.ScaleTransform(1.0f, -1.0f); // 變更方向
            // 
            g.DrawLine(Pens.Black, -30, 0, thePanel.Width, 0);
            g.DrawLine(Pens.Black, 0, -30, 0, thePanel.Height);
            //
            g.ScaleTransform(4.0f, 4.0f); // 放大４倍

            return g;
        }

        /// <summary>
        /// sub-sequence
        /// </summary>
        static protected int[] InitIntArray(int size)
        {
            //# init. int array
            int[] datas = new int[100];
            for (int i = 0; i < 100; i++)
                datas[i] = i; // 0 ~ 99

            // randomize
            Random r = new Random();
            for (int i = 0; i < 100; i++)
            {
                int i1 = r.Next(100);
                int i2 = r.Next(100);
                // swap
                int tmp = datas[i1];
                datas[i1] = datas[i2];
                datas[i2] = tmp;
            }

            return datas;
        }

        //static protected void Swap(int[] datas, int i, int j)
        //{
        //    int tmp = datas[i];
        //    datas[i] = datas[j];
        //    datas[j] = tmp;
        //}

        static protected void Swap(int[] datas, int i, int j)
        {
            // before swap
            _g.DrawLine(Pens.LightGray, i, 99, i, 0);
            _g.DrawLine(Pens.LightGray, j, 99, j, 0);

            // swap
            int tmp = datas[i];
            datas[i] = datas[j];
            datas[j] = tmp;

            // Draw Data
            _g.DrawLine(_pen, i, datas[i], i, 0);
            _g.DrawLine(_pen, j, datas[j], j, 0);

            // wait a short time
            Thread.Sleep(_sleepTimespan);
        }

        static protected void DrawDatas(int[] datas, Pen pen)
        {
            // draw
            _g.FillRectangle(Brushes.LightGray, 0, 0, 100, 100);
            for (int i = 0; i < 100; i++)
                _g.DrawLine(pen, i, datas[i], i, 0);               
        }

        static protected void DrawDatas(int[] datas)
        {
            DrawDatas(datas, _pen);
        }

        // 插入排序法
        static protected void InsertionSort(int[] datas)
        {
            //從未排序數列取出一元素。
            //由後往前和已排序數列元素比較，直到遇到不大於自己的元素並插入此元素之後；若都沒有則插入在最前面。
            //重複以上動作直到未排序數列全部處理完成。

            int[] sortedDatas = new int[datas.Length];
            for (int i = 0; i < datas.Length; i++)
            {
                // 取出
                int c = datas[i];
                _g.DrawLine(Pens.LightGray, i, 99, i, 0);

                // 比較
                int insertIndex = i;
                for (; insertIndex > 0 && sortedDatas[insertIndex - 1] > c; insertIndex--) ; // compare

                // 插入-shift
                for (int j = i; j > insertIndex; j--)
                {
                    _g.DrawLine(Pens.LightGray, j-1, 99, j-1, 0);
                    sortedDatas[j] = sortedDatas[j - 1]; //
                    _g.DrawLine(_pen, j, sortedDatas[j], j, 0);

                    // wait a short time
                    Thread.Sleep(_sleepTimespan);
                }

                // 插入-new
                sortedDatas[insertIndex] = c;
                _g.DrawLine(Pens.Green, insertIndex, sortedDatas[insertIndex], insertIndex, 0);
            }
        }

        // 泡沫排序法
        static protected void BubbleSort(int[] datas)
        {
            for (int i = 0; i < datas.Length - 1; i++)
                for (int j = i + 1; j < datas.Length; j++)
                    if (datas[i] > datas[j]) // compare
                    {
                        // swap
                        Swap(datas, i, j);
                    }
        }

        // 選擇排序法
        static protected void SelectionSort(int[] datas)
        {
            //Function selectionSort(Type data[1..n])
            //    Index i, j, max  --- or min
            //    For i from 1 to n do
            //        max = i
            //        For j from i + 1 to n do
            //            If data[j] > data[max] then
            //                max = j
            //
            //        Exchange data[i] and data[max]
            //End

            //# Index i, j, max  --- or min
            int minIndex;
            for (int i = 0; i < datas.Length; i++)
            {
                minIndex = i;
                for(int j = i+1; j < datas.Length; j++)
                    if(datas[j] < datas[minIndex]) // compare
                        minIndex = j;

                //# Exchange data[i] and data[max]
                Swap(datas, i, minIndex);
            }
        }

        // 快速排序法
        static protected void QuickSort(int[] datas, int p, int r)
        {
            //Function QuickSort(A, p, r)
            //    IF p < r Then
            //       q = PARTITION(A, p, r)
            //       QuickSort(A, p, q-1)
            //       QuickSort(A, q+1, r)
            //End

            if (p < r)
            {
                int q = QuickSortPartition(datas, p, r);
                QuickSort(datas, p, q - 1);
                QuickSort(datas, q + 1, r);
            }
        }

        static protected int QuickSortPartition(int[] datas, int p, int r)
        {
            // Partition subarray A[p..r] by the following procedure:
            //1   x <- A[r]
            //2   i  <- p – 1
            //3   for j <- p to r -1 
            //4   	do if A[j] ≤ x
            //5   		then i <- i + 1
            //6   		        exchange A[i] <-> A[j]
            //7   exchange A[i +1] <-> A[r] 
            //8   return  i +1

            int x = datas[r];
            int i = p - 1;
            for (int j = p; j < r; j++)
                if (datas[j] <= x)
                {
                    i++; // i = i + 1;
                    // exchange A[i]<->A[j]
                    Swap(datas, i, j);
                }

            // exchange A[i +1] <-> A[r]
            Swap(datas, i+1, r);
            return i + 1;
        }

        // 合併排序法
        static protected void MergeSort(int[] datas)
        {
            #region algorithm
            //TopDownMergeSort(A[], B[], n)
            //{
            //    TopDownSplitMerge(A, 0, n, B);
            //}
            #endregion

            int[] mergedDatas = new int[datas.Length];
            TopDownSplitMerge(datas, 0, datas.Length -1, mergedDatas);
        }

        static protected void TopDownSplitMerge(int[] datas, int iBegin, int iEnd, int[] mergedDatas)
        {
            #region algorithm
            //TopDownSplitMerge(A[], iBegin, iEnd, B[])
            //{
            //    if(iEnd - iBegin < 2)                       // if run size == 1
            //        return;                                 //   consider it sorted
            //    // recursively split runs into two halves until run size == 1,
            //    // then merge them and return back up the call chain
            //    iMiddle = (iEnd + iBegin) / 2;              // iMiddle = mid point
            //    TopDownSplitMerge(A, iBegin,  iMiddle, B);  // split / merge left  half
            //    TopDownSplitMerge(A, iMiddle, iEnd,    B);  // split / merge right half
            //    TopDownMerge(A, iBegin, iMiddle, iEnd, B);  // merge the two half runs
            //    CopyArray(B, iBegin, iEnd, A);              // copy the merged runs back to A
            //}
            #endregion

            Debug.WriteLine(string.Format("TopDownSplitMerge iBegin ~ iEnd : {0} ~ {1}", iBegin, iEnd));

            if (iEnd <= iBegin) // consider it sorted.
                return;

            int iMiddle = (iEnd + iBegin) / 2;
            TopDownSplitMerge(datas, iBegin, iMiddle, mergedDatas); // split / merge left  half
            TopDownSplitMerge(datas, iMiddle+1, iEnd, mergedDatas); // split / merge right half
            Merge(datas, iBegin, iMiddle, iEnd, mergedDatas);
            CopyArray(mergedDatas, iBegin, iEnd, datas); // // copy the merged runs back to A
        }

        static protected void Merge(int[] A, int iBegin, int iMiddle, int iEnd, int[] B)
        {
            #region algorithm
            //TopDownMerge(A[], iBegin, iMiddle, iEnd, B[])
            //{
            //    i0 = iBegin, i1 = iMiddle;
            // 
            //    // While there are elements in the left or right runs
            //    for (j = iBegin; j < iEnd; j++) {
            //        // If left run head exists and is <= existing right run head.
            //        if (i0 < iMiddle && (i1 >= iEnd || A[i0] <= A[i1]))
            //            B[j] = A[i0++];  // Increment i0 after using it as an index.
            //        else
            //            B[j] = A[i1++];  // Increment i1 after using it as an index.
            //    }
            //}
            #endregion

            // before 
            _g.FillRectangle(Brushes.LightGray, iBegin, 0, iEnd - iBegin +1 , 100);
            _g.DrawLine(Pens.LightGray, 0, -5, 100, -5);
            _g.DrawLine(Pens.Green, iBegin, -5, iEnd + 1, -5);

            int i0 = iBegin;
            int i1 = iMiddle+1;
            int j = iBegin;
            while(i0 <= iMiddle && i1 <= iEnd)
            {
                if (A[i0] <= A[i1])
                {
                    _g.DrawLine(Pens.LightGray, i0, -5, i0+1, -5);
                    _g.DrawLine(_pen, j, A[i0], j, 0);
                    B[j++] = A[i0++];
                    Thread.Sleep(_sleepTimespan);
                }
                else
                {
                    _g.DrawLine(Pens.LightGray, i1, -5, i1+1, -5);
                    _g.DrawLine(_pen, j, A[i1], j, 0);
                    B[j++] = A[i1++];
                    Thread.Sleep(_sleepTimespan);
                }
            }

            while (i0 <= iMiddle)
            {
                _g.DrawLine(Pens.LightGray, i0, -5, i0+1, -5);
                _g.DrawLine(_pen, j, A[i0], j, 0);
                B[j++] = A[i0++];
                Thread.Sleep(_sleepTimespan);
            }

            while (i1 <= iEnd)
            {
                _g.DrawLine(Pens.LightGray, i1, -5, i1+1, -5);
                _g.DrawLine(_pen, j, A[i1], j, 0);
                B[j++] = A[i1++];
                Thread.Sleep(_sleepTimespan);
            }
        }

        static protected void CopyArray(int[] dataFrom, int iBegin, int iEnd, int[] dataTo)
        {
            for (int i = iBegin; i <= iEnd; i++)
                dataTo[i] = dataFrom[i];
        }

        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //# prepare resource
            _g = SubPreapreSorting(panel1); // 設定座標系統
            _pen = Pens.Cyan;
            _sleepTimespan = (int)this.numSleepTimespan.Value;

            //# init. int array
            int[] datas = InitIntArray(100);

            // draw datas first
            DrawDatas(datas, Pens.Blue);

            // sort -------------------
            InsertionSort(datas);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //# prepare resource
            _g = SubPreapreSorting(panel1); // 設定座標系統
            _pen = Pens.Cyan;
            _sleepTimespan = (int)this.numSleepTimespan.Value;

            //# init. int array
            int[] datas = InitIntArray(100);

            // draw datas first
            DrawDatas(datas, Pens.Blue);

            // sort -------------------
            BubbleSort(datas);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //# prepare resource
            _g = SubPreapreSorting(panel1); // 設定座標系統
            _pen = Pens.Cyan;
            _sleepTimespan = (int)this.numSleepTimespan.Value;

            //# init. int array
            int[] datas = InitIntArray(100);

            // draw datas first
            DrawDatas(datas, Pens.Blue);

            // sort -------------------
            SelectionSort(datas);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //# prepare resource
            _g = SubPreapreSorting(panel1); // 設定座標系統
            _pen = Pens.Cyan;
            _sleepTimespan = (int)this.numSleepTimespan.Value;

            //# init. int array
            int[] datas = InitIntArray(100);

            // draw datas first
            DrawDatas(datas, Pens.Blue);

            // sort -------------------
            QuickSort(datas, 0, 99);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //# prepare resource
            _g = SubPreapreSorting(panel1); // 設定座標系統
            _pen = Pens.Cyan;
            _sleepTimespan = (int)this.numSleepTimespan.Value;
            
            //# init. int array
            int[] datas = InitIntArray(100);

            // draw datas first
            DrawDatas(datas, Pens.Blue);

            // sort -------------------
            MergeSort(datas);

            //DrawDatas(datas, Pens.Cyan);
        }


    }
}
