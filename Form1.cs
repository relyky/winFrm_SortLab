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
using NCCUCS.AspectW;

namespace winFrm_SortLab
{
    public partial class Form1 : Form
    {
        #region drawing resource

        static internal protected Pen _pen = Pens.Cyan;
        static internal protected Graphics _g = null;
        static internal protected int _sleepTimespan = 2;
        
        //static internal protected int[] _datas = null;
        static internal protected AValue[] _ADatas = null;

        #endregion

        #region tools

        static public void PrepareResource(Form1 frm1)
        {
            //# prepare resource
            _g = SubPreapreSorting(frm1.panel1); // 設定座標系統
            _pen = Pens.Cyan;
            _sleepTimespan = (int)frm1.numSleepTimespan.Value;

            // reset counting label.
            frm1.lblCompare.Text = "比較次數：？？？";
            frm1.lblCompare.Update();
            frm1.lblExchange.Text = "交換次數：？？？";
            frm1.lblExchange.Update();
            frm1.lblUpdate.Text = "移動次數：？？？";
            frm1.lblUpdate.Update();
            frm1.lblCounting.Text = "定址計算：？？？";
            frm1.lblCounting.Update();
            frm1.lblTotal.Text = "次數總計：？？？";
            frm1.lblTotal.Update();
        }

        static public void ShowCount(Form1 frm1)
        {
            //# show counter
            frm1.lblCompare.Text = string.Format("比較：{0:N0}", AValueEx._compareCount);
            frm1.lblExchange.Text = string.Format("交換：{0:N0}", AValueEx._exchangeCount);
            frm1.lblUpdate.Text = string.Format("移動：{0:N0}", AValueEx._updateCount);
            frm1.lblCounting.Text = string.Format("定址：{0:N0}", AValueEx._countingCount);
            uint total = AValueEx._compareCount + AValueEx._exchangeCount + AValueEx._updateCount + AValueEx._countingCount;
            frm1.lblTotal.Text = string.Format("總計：{0:N0}", total);
        }

        /// <summary>
        /// sub-sequence
        /// </summary>
        static public Graphics SubPreapreSorting(Panel thePanel)
        {
            //# 設定座標系統
            //Graphics g = e.Graphics;
            Graphics g = thePanel.CreateGraphics();
            g.TranslateTransform(30, thePanel.Height - 30); // 變更原點
            g.ScaleTransform(1.0f, -1.0f); // 變更方向
            // 
            g.DrawLine(Pens.Black, -30, -3, thePanel.Width, -3);
            g.DrawLine(Pens.Black, -3, -30, -3, thePanel.Height);
            //
            g.ScaleTransform(4.0f, 4.0f); // 放大４倍

            // 顏色量尺
            for (int i = 0; i < 105; i++)
                g.DrawLine(new Pen(AValueEx.MapColor(i)), -6, i, -3, i);

            return g;
        }

        static protected int[] GenRandArray()
        {
            const int size = 100;

            //# init. int array
            int[] datas = new int[size];
            for (int i = 0; i < size; i++)
                datas[i] = i; // 0 ~ 99

            // randomize
            Random r = new Random();
            for (int i = 0; i < size; i++)
            {
                int i1 = r.Next(size);
                int i2 = r.Next(size);
                // swap
                int tmp = datas[i1];
                datas[i1] = datas[i2];
                datas[i2] = tmp;
            }

            return datas;
        }

        static protected int[] GenSortedArray(bool reverse)
        {
            //# init. int array
            int[] datas = new int[100];
            for (int i = 0; i < 100; i++)
                datas[i] = reverse ? 99 - i : i; // 0 ~ 99

            return datas;
        }

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
            // reset data canvas
            ResetDataCanvas();

            // draw
            for (int i = 0; i < datas.Length; i++)
                _g.DrawLine(pen, i, datas[i], i, 0);               
        }

        static protected void DrawData(int[] datas, int index)
        {
            _g.DrawLine(_pen, index, datas[index], index, 0);
        }

        static public void DrawC(int x, int v)
        {
            // visualize C.
            Pen pen = new Pen(_GetColor(v));
            _g.DrawLine(pen, x, -5, x, -4);
        }

        static public void ResetDataCanvas()
        {
            //ResetDataCanvas(0, 0, 99, 99);
            DrawDataCanvas(Color.LightGray, 0, 0, 99, 99);
        }

        static public void ResetDataCanvas(int x1, int y1, int x2, int y2)
        {
            //_g.DrawRectangle(Pens.LightGray, x1, y1, x2 - x1, y2 - y1);
            //_g.FillRectangle(Brushes.LightGray, x1, y1, x2 - x1, y2 - y1);
            DrawDataCanvas(Color.LightGray, x1, y1, x2, y2);
        }

        static public void DrawDataCanvas(Color color, int x1, int y1, int x2, int y2)
        {
            Brush brush = new SolidBrush(color); 
            Pen pen = new Pen(brush);

            _g.DrawRectangle(pen, x1, y1, x2 - x1, y2 - y1);
            _g.FillRectangle(brush, x1, y1, x2 - x1, y2 - y1);
        }

        /// <summary>
        /// 依值取得 Color 階層，值越大顏色越深。
        /// </summary>
        static protected Color _GetColor(int value)
        {
            return Color.FromArgb(240 - value * 2, 128, 240 - value * 2); // r,g,b, 值越大顏色越重
            //return _MapColor2(value);
        }

        #region backup

        //static protected Color _MapColor(int index) /* 0 ~ 100 */
        //{
        //    Color[] colors = new Color[]
        //    {
        //        Color.WhiteSmoke, Color.LightBlue, Color.LightCoral, Color.LightCyan, Color.LightGoldenrodYellow,
        //        Color.LightGray, Color.LightGreen, Color.LightPink, Color.LightSalmon, Color.LightSeaGreen,
        //        Color.LightSkyBlue, Color.LightSlateGray, Color.LightSteelBlue, Color.LightYellow, Color.Lime,
        //        Color.LimeGreen, Color.MidnightBlue, Color.MediumAquamarine, Color.MediumBlue, Color.MediumOrchid,
        //        Color.MediumPurple, Color.MediumSeaGreen, Color.MediumSlateBlue, Color.MediumSpringGreen, Color.MediumTurquoise,
        //        Color.MediumVioletRed, Color.MintCream, Color.MistyRose, Color.Moccasin, Color.NavajoWhite, 
        //        Color.Navy, Color.Lavender, Color.LavenderBlush, Color.LawnGreen, Color.LemonChiffon, 
        //        Color.Magenta, Color.Maroon, Color.OldLace, Color.Olive, Color.OliveDrab, 
        //        Color.Orange, Color.OrangeRed, Color.Orchid, Color.PaleGoldenrod, Color.PaleGreen,
        //        Color.PaleTurquoise, Color.PaleVioletRed, Color.PapayaWhip, Color.PeachPuff, Color.Peru,
        //        Color.Pink, Color.Plum, Color.PowderBlue, Color.Purple, Color.Red, 
        //        Color.AliceBlue, Color.AntiqueWhite, Color.Aqua, Color.Aquamarine, Color.Azure,
        //        Color.Beige, Color.Bisque, Color.BlanchedAlmond, Color.Blue, Color.BlueViolet, // 13
        //        Color.CadetBlue, Color.Chartreuse, Color.Chocolate, Color.Coral, Color.CornflowerBlue,
        //        Color.Cornsilk, Color.Crimson, Color.Cyan, Color.Firebrick, Color.FloralWhite,
        //        Color.ForestGreen, Color.Gainsboro, Color.GhostWhite, Color.Gold, Color.Goldenrod,
        //        Color.DarkBlue, Color.DarkCyan, Color.DarkGoldenrod, Color.DarkGray, Color.DarkGreen,
        //        Color.DarkKhaki, Color.DarkMagenta, Color.DarkOliveGreen, Color.DarkOrange, Color.DarkOrchid,
        //        Color.DarkRed, Color.DarkSalmon, Color.DarkSeaGreen, Color.DarkSlateBlue, Color.DarkSlateGray,
        //        Color.DarkTurquoise, Color.DarkViolet, Color.DeepPink, Color.DeepSkyBlue, Color.Black, // 20
        //        Color.Black // 21
        //    };

        //    return colors[index];
        //}

        //static protected Color _MapColor2(int index) /* 0 ~ 100 */
        //{
        //    int y = 0;
        //    if (index == 0)
        //        return Color.WhiteSmoke;

        //    y = index;
        //    if (index <= 20) // green
        //        return Color.FromArgb(0, 255 - 5 * y, 0);

        //    y = index - 20; // yellow
        //    if(index <= 40)
        //        return Color.FromArgb(255 - 5 * y, 255 - 5 * y, 63);

        //    y = index - 40; // pink
        //    if(index <= 60)
        //        return Color.FromArgb(255 - 5 * y, 128 - y, 128);

        //    y = index - 60;
        //    if (index <= 80) // blue
        //        return Color.FromArgb(0, 0, 255 - 5 * y);

        //    y = index - 80;  // red
        //    if (index <= 100)
        //        return Color.FromArgb(255 - 5 * y, 0, 0);

        //    return Color.Black;
        //}

        //static protected Color _MapColor3(int index) /* 0 ~ 100 */
        //{
        //     // (x % 5)*5 + (x / 5)
        //     // x % 5 * 20 + x / 5

        //    return _MapColor2(index % 5 * 20 + index / 5);
        //}

        #endregion

        #region backup

        //// 插入排序法
        //static protected void InsertionSort(int[] datas)
        //{
        //    //從未排序數列取出一元素。
        //    //由後往前和已排序數列元素比較，直到遇到不大於自己的元素並插入此元素之後；若都沒有則插入在最前面。
        //    //重複以上動作直到未排序數列全部處理完成。

        //    int[] sortedDatas = new int[datas.Length];
        //    for (int i = 0; i < datas.Length; i++)
        //    {
        //        // 取出
        //        int c = datas[i];
        //        _g.DrawLine(Pens.LightGray, i, 99, i, 0);

        //        // 比較
        //        int insertIndex = i;
        //        for (; insertIndex > 0 && sortedDatas[insertIndex - 1] > c; insertIndex--) ; // compare

        //        // 插入-shift
        //        for (int j = i; j > insertIndex; j--)
        //        {
        //            _g.DrawLine(Pens.LightGray, j-1, 99, j-1, 0);
        //            sortedDatas[j] = sortedDatas[j - 1]; //
        //            _g.DrawLine(_pen, j, sortedDatas[j], j, 0);

        //            // wait a short time
        //            Thread.Sleep(_sleepTimespan);
        //        }

        //        // 插入-new
        //        sortedDatas[insertIndex] = c;
        //        _g.DrawLine(Pens.Green, insertIndex, sortedDatas[insertIndex], insertIndex, 0);
        //    }

        //    // copy back
        //    CopyArray(sortedDatas, 0, datas.Length - 1, datas);
        //}

        //// 泡沫排序法
        //static protected void BubbleSort(int[] datas)
        //{
        //    for (int i = 0; i < datas.Length - 1; i++)
        //        for (int j = i + 1; j < datas.Length; j++)
        //            if (datas[i] > datas[j]) // compare
        //            {
        //                // swap
        //                Swap(datas, i, j);
        //            }
        //}

        //// 選擇排序法
        //static protected void SelectionSort(int[] datas)
        //{
        //    //Function selectionSort(Type data[1..n])
        //    //    Index i, j, max  --- or min
        //    //    For i from 1 to n do
        //    //        max = i
        //    //        For j from i + 1 to n do
        //    //            If data[j] > data[max] then
        //    //                max = j
        //    //
        //    //        Exchange data[i] and data[max]
        //    //End

        //    //# Index i, j, max  --- or min
        //    int minIndex;
        //    for (int i = 0; i < datas.Length; i++)
        //    {
        //        minIndex = i;
        //        for(int j = i+1; j < datas.Length; j++)
        //            if(datas[j] < datas[minIndex]) // compare
        //                minIndex = j;

        //        //# Exchange data[i] and data[max]
        //        Swap(datas, i, minIndex);
        //    }
        //}

        //// 快速排序法
        //static protected void QuickSort(int[] datas, int p, int r)
        //{
        //    //Function QuickSort(A, p, r)
        //    //    IF p < r Then
        //    //       q = PARTITION(A, p, r)
        //    //       QuickSort(A, p, q-1)
        //    //       QuickSort(A, q+1, r)
        //    //End

        //    if (p < r)
        //    {
        //        int q = QuickSortPartition(datas, p, r);
        //        QuickSort(datas, p, q - 1);
        //        QuickSort(datas, q + 1, r);
        //    }
        //}

        //static protected int QuickSortPartition(int[] datas, int p, int r)
        //{
        //    // Partition subarray A[p..r] by the following procedure:
        //    //1   x <- A[r]
        //    //2   i  <- p – 1
        //    //3   for j <- p to r -1 
        //    //4   	do if A[j] ≤ x
        //    //5   		then i <- i + 1
        //    //6   		        exchange A[i] <-> A[j]
        //    //7   exchange A[i +1] <-> A[r] 
        //    //8   return  i +1

        //    int x = datas[r];
        //    int i = p - 1;
        //    for (int j = p; j < r; j++)
        //        if (datas[j] <= x)
        //        {
        //            i++; // i = i + 1;
        //            // exchange A[i]<->A[j]
        //            Swap(datas, i, j);
        //        }

        //    // exchange A[i +1] <-> A[r]
        //    Swap(datas, i+1, r);
        //    return i + 1;
        //}

        //static protected void QuickSortRand(int[] datas, int p, int r)
        //{
        //    #region algorithm
        //    //RANDOMIZED_QUICKSORT(A,p,r)
        //    //1  if p < r then
        //    //2     q <- RANDOMIZED_PARTITION(A,p,r)
        //    //3     RANDOMIZED_QUICKSORT(A, p, q-1)
        //    //4     RANDOMIZED_QUICKSORT(A,q+1,r)
        //    #endregion

        //    if (p < r)
        //    {
        //        int q = QuickSortRandPartition(datas, p, r);
        //        QuickSortRand(datas, p, q - 1);
        //        QuickSortRand(datas, q + 1, r);
        //    }
        //}

        //static protected int QuickSortRandPartition(int[] datas, int p, int r)
        //{
        //    #region algorithm
        //    //RANDOMIZED_PARTITION(A,p,r)
        //    //1   i <- RANDOM(p,r)
        //    //2   exchange A[r]<->A[i]
        //    //3   return PARTITION(A,p,r)
        //    #endregion

        //    // randomize
        //    Random rand = new Random();
        //    int i = p + rand.Next(r - p);
        //    Swap(datas, r, i);
        //    return QuickSortPartition(datas, p, r);
        //}

        //// 合併排序法
        //static protected void MergeSort(int[] datas)
        //{
        //    #region algorithm
        //    //TopDownMergeSort(A[], B[], n)
        //    //{
        //    //    TopDownSplitMerge(A, 0, n, B);
        //    //}
        //    #endregion

        //    int[] mergedDatas = new int[datas.Length];
        //    TopDownSplitMerge(datas, 0, datas.Length -1, mergedDatas);
        //}

        //static protected void TopDownSplitMerge(int[] datas, int iBegin, int iEnd, int[] mergedDatas)
        //{
        //    #region algorithm
        //    //TopDownSplitMerge(A[], iBegin, iEnd, B[])
        //    //{
        //    //    if(iEnd - iBegin < 2)                       // if run size == 1
        //    //        return;                                 //   consider it sorted
        //    //    // recursively split runs into two halves until run size == 1,
        //    //    // then merge them and return back up the call chain
        //    //    iMiddle = (iEnd + iBegin) / 2;              // iMiddle = mid point
        //    //    TopDownSplitMerge(A, iBegin,  iMiddle, B);  // split / merge left  half
        //    //    TopDownSplitMerge(A, iMiddle, iEnd,    B);  // split / merge right half
        //    //    TopDownMerge(A, iBegin, iMiddle, iEnd, B);  // merge the two half runs
        //    //    CopyArray(B, iBegin, iEnd, A);              // copy the merged runs back to A
        //    //}
        //    #endregion

        //    Debug.WriteLine(string.Format("TopDownSplitMerge iBegin ~ iEnd : {0} ~ {1}", iBegin, iEnd));

        //    if (iEnd <= iBegin) // consider it sorted.
        //        return;

        //    int iMiddle = (iEnd + iBegin) / 2;
        //    TopDownSplitMerge(datas, iBegin, iMiddle, mergedDatas); // split / merge left  half
        //    TopDownSplitMerge(datas, iMiddle+1, iEnd, mergedDatas); // split / merge right half
        //    Merge(datas, iBegin, iMiddle, iEnd, mergedDatas);
        //    CopyArray(mergedDatas, iBegin, iEnd, datas); // // copy the merged runs back to A
        //}

        //static protected void Merge(int[] A, int iBegin, int iMiddle, int iEnd, int[] B)
        //{
        //    #region algorithm
        //    //TopDownMerge(A[], iBegin, iMiddle, iEnd, B[])
        //    //{
        //    //    i0 = iBegin, i1 = iMiddle;
        //    // 
        //    //    // While there are elements in the left or right runs
        //    //    for (j = iBegin; j < iEnd; j++) {
        //    //        // If left run head exists and is <= existing right run head.
        //    //        if (i0 < iMiddle && (i1 >= iEnd || A[i0] <= A[i1]))
        //    //            B[j] = A[i0++];  // Increment i0 after using it as an index.
        //    //        else
        //    //            B[j] = A[i1++];  // Increment i1 after using it as an index.
        //    //    }
        //    //}
        //    #endregion

        //    // before 
        //    //_g.FillRectangle(Brushes.LightGray, iBegin, 0, iEnd - iBegin + 1 , 99);
        //    ResetDataCanvas(iBegin, 0, iEnd, 99);
        //    //_g.DrawLine(Pens.LightGray, 0, -5, , -5);
        //    ResetDataCanvas(0, -5, 99, -4);
        //    //_g.DrawLine(Pens.Green, iBegin, -5, iEnd, -4);
        //    DrawDataCanvas(Color.Green, iBegin, -5, iEnd, -4);

        //    int i0 = iBegin;
        //    int i1 = iMiddle+1;
        //    int j = iBegin;
        //    while(i0 <= iMiddle && i1 <= iEnd)
        //    {
        //        if (A[i0] <= A[i1])
        //        {
        //            _g.DrawLine(Pens.LightGray, i0, -5, i0, -4);
        //            _g.DrawLine(_pen, j, A[i0], j, 0);
        //            B[j++] = A[i0++];
        //            Thread.Sleep(_sleepTimespan);
        //        }
        //        else
        //        {
        //            _g.DrawLine(Pens.LightGray, i1, -5, i1, -4);
        //            _g.DrawLine(_pen, j, A[i1], j, 0);
        //            B[j++] = A[i1++];
        //            Thread.Sleep(_sleepTimespan);
        //        }
        //    }

        //    while (i0 <= iMiddle)
        //    {
        //        _g.DrawLine(Pens.LightGray, i0, -5, i0, -4);
        //        _g.DrawLine(_pen, j, A[i0], j, 0);
        //        B[j++] = A[i0++];
        //        Thread.Sleep(_sleepTimespan);
        //    }

        //    while (i1 <= iEnd)
        //    {
        //        _g.DrawLine(Pens.LightGray, i1, -5, i1, -4);
        //        _g.DrawLine(_pen, j, A[i1], j, 0);
        //        B[j++] = A[i1++];
        //        Thread.Sleep(_sleepTimespan);
        //    }
        //}

        //static protected void CopyArray(int[] dataFrom, int iBegin, int iEnd, int[] dataTo)
        //{
        //    for (int i = iBegin; i <= iEnd; i++)
        //        dataTo[i] = dataFrom[i];
        //}

        //static protected int Min(int a, int b)
        //{
        //    return a > b ? b : a;
        //}

        //// Bottom Up Merge Sort
        //static protected void BottomUpMergeSort(int[] datas)
        //{
        //    /* let 'datas' as array A */
        //    /* let 'mergedDatas' as array B*/
        //    int[] mergedDatas = new int[datas.Length]; 

        //    /* Each 1-element run in A is already "sorted". */
        //    /* Make successively longer sorted runs of length 2, 4, 8, 16... until whole array is sorted. */
        //    int n = datas.Length - 1;
        //    for (int width = 1; width <= n; width = 2 * width)
        //    {
        //        /* Array A is full of runs of length width. */
        //        for (int i = 0; i <= n; i = i + 2 * width)
        //        {
        //            /* Merge two runs: A[i:i+width-1] and A[i+width:i+2*width-1] to B[] */
        //            /* or copy A[i:n-1] to B[] ( if(i+width >= n) ) */
        //            Merge(datas, i, Min(i + width -1, n), Min(i + 2 * width -1, n), mergedDatas);
        //        }

        //        /* Now work array B is full of runs of length 2*width. */
        //        /* Copy array B to array A for next iteration. */
        //        /* A more efficient implementation would swap the roles of A and B */
        //        CopyArray(mergedDatas, 0, n, datas);

        //        /* Now array A is full of runs of length 2*width. */
        //    }
        //}

        //// 堆積排序法
        //static protected void HeapSort(int[] datas)
        //{
        //    #region algorithm
        //    // Heapsort(A)
        //    //1  Build-Max-Heap(A)
        //    //2  for i <- length[A] down to 2
        //    //3     do exchange A[1]<->A[i]
        //    //4          heap-size[A] <- heap-size[A] -1
        //    //5          Max-Heapify(A,1)
        //    #endregion

        //    BuildMaxHeap(datas);

        //    int heap_size = datas.Length;
        //    for (int i = datas.Length - 1; i >= 1; i--)
        //    {
        //        Swap(datas, i, 0); // zero-base array.
        //        heap_size--;
        //        MaxHeapify(datas, heap_size, 0);
        //    }
        //}

        //static protected void BuildMaxHeap(int[] datas)
        //{
        //    #region algorithm
        //    //Build-Max-Heap(A)
        //    //1   heap-size[A] <- length[A]
        //    //2   for i <- floor(length[A]/2) downto 1
        //    //3  	    do Max-Heapify(A, i)
        //    #endregion

        //    int heap_size = datas.Length;
        //    for (int i = (datas.Length - 1) / 2; i >= 0; i--)
        //        MaxHeapify(datas, heap_size, i);
        //}

        //static protected void MaxHeapify(int[] datas, int heap_size, int index)
        //{
        //    #region algorithm
        //    // Max-Heapify (A, i )
        //    //1 l <- Left (i )
        //    //2 r <- Right(i )
        //    //3 if l ≤ heap-size[A] and A[l] > A[i ]
        //    //4 		then largest <- l
        //    //5 		else largest <- i
        //    //6 if r ≤ heap-size[A] and A[r] > A[largest]
        //    //7		then largest <- r
        //    //8 if largest <> i
        //    //9  		then exchange A[i] <-> A[largest]
        //    //10 		     Max-Heapify (A, largest)
        //    #endregion

        //    //int heap_size = datas.Length;
        //    int l = HeapLeft(index);
        //    int r = HeapRight(index); 

        //    int largest = index;
        //    if (l < heap_size && datas[l] > datas[largest])
        //        largest = l;
        //    if (r < heap_size && datas[r] > datas[largest])
        //        largest = r;

        //    if (largest != index)
        //    {
        //        Swap(datas, index, largest);
        //        MaxHeapify(datas, heap_size, largest);
        //    }
        //}

        //static protected int HeapLeft(int index)
        //{
        //    //Left child of A[i ] = A[2 * i]. --- 1 base
        //    return ((index + 1) * 2 - 1); 
        //}

        //static protected int HeapRight(int index)
        //{
        //    //Right child of A[i] = A[2 * i + 1].
        //    return HeapLeft(index) + 1;
        //}

        //// 計數排序法 Counting Sort
        //static protected void CountingSort(int[] datas)
        //{
        //    #region algorithm
        //    //CountingSort(A,B,k)
        //    //1.for i <- 0 to k
        //    //2.  do C[i] <- 0
        //    //3.for j <- 1 to length[A]
        //    //4.  do C[A[j]] <- C[A[j]]+1
        //    //5. // C[i] now contains the number of elements equal to i.
        //    //6.for i <- 1 to k
        //    //7.  do C[i] <- C[i] + C[i-1]
        //    //8. // C[i] now contains the number of elements less than or equal to i.
        //    //9. for j <- length[A] downto 1
        //    //10.  do B[C[A[j]]] <- A[j]
        //    //11      C[A[j]] <- C[A[j]] - 1
        //    #endregion

        //    //# duplation as A in place of datas[]. and let datas[] as B;
        //    int[] A = new int[datas.Length];
        //    for(int i = 0; i < datas.Length; i++)
        //        A[i] = datas[i];
            
        //    //## BEGIN to do counting sort.
        //    //# step 1,2.
        //    int[] C = new int[A.Length];
        //    for (int i = 0; i < C.Length; i++) // k == length[A], in this case.
        //        C[i] = 0; // do C[i] <- 0

        //    // visualize C.
        //    ResetDataCanvas(0, -5, 99, -4);

        //    //# step 3,4.
        //    for (int i = 0; i < A.Length; i++)
        //    {
        //        C[A[i]]++;

        //        // visualize C.
        //        DrawC(A[i], C[A[i]]);
        //        Thread.Sleep(_sleepTimespan);
        //    }

        //    //# step 6,7.
        //    for (int i = 1; i < C.Length; i++)
        //    {
        //        C[i] = C[i] + C[i - 1];
                
        //        // visualize C.
        //        DrawC(i, C[i]);
        //        Thread.Sleep(_sleepTimespan);
        //    }

        //    //# step 8,9,10.
        //    // befroe
        //    ResetDataCanvas();

        //    for (int i = A.Length - 1; i >= 0; i--)
        //    {
        //        // 'datas' as B
        //        //datas[C[A[i]] - 1] = A[i]; // zero base array.
        //        int di = C[A[i]] - 1; // zero base array.
        //        datas[di] = A[i]; 
        //        C[A[i]]--;

        //        // visualize
        //        DrawData(datas, di);
        //        DrawC(A[i], C[A[i]]);
        //        Thread.Sleep(_sleepTimespan);
        //    }

        //}

        //// 基數排序法 Radix sort
        //static protected void RadixSort(int[] datas)
        //{
        //    #region algorithm
        //    //#define MAX 20
        //    //#define SHOWPASS
        //    //#define BASE 10
        //    //void radixsort(int *a, int n)
        //    //{
        //    //  int i, b[MAX], m = a[0], exp = 1;
        //    //  for (i = 1; i < n; i++)
        //    //  {
        //    //    if (a[i] > m)
        //    //      m = a[i];
        //    //  }
        //    // 
        //    //  while (m / exp > 0)
        //    //  {
        //    //    int bucket[BASE] ={  0 };
        //    //    for (i = 0; i < n; i++)
        //    //      bucket[(a[i] / exp) % BASE]++;
        //    //    for (i = 1; i < BASE; i++)
        //    //      bucket[i] += bucket[i - 1];
        //    //    for (i = n - 1; i >= 0; i--)
        //    //      b[--bucket[(a[i] / exp) % BASE]] = a[i];
        //    //    for (i = 0; i < n; i++)
        //    //      a[i] = b[i];
        //    //    exp *= BASE;
        //    // 
        //    //    #ifdef SHOWPASS
        //    //      printf("\nPASS   : ");
        //    //      print(a, n);
        //    //    #endif
        //    //  }
        //    //}
        //    #endregion

        //    //## parameters
        //    const int BASE = 10;

        //    //## get max value
        //    int m = 99; // datas.Max();

        //    // bucket sort
        //    int n = datas.Length;
        //    int[] b = new int[n]; // as sorted datas.
        //    int exp = 1;
        //    while (m / exp > 0)
        //    {
        //        //## bucket sort
        //        int[] bucket = new int[BASE];

        //        // visualize bucket.
        //        ResetDataCanvas(0, -5, 99, -4);

        //        for (int i = 0; i < n; i++)
        //        {
        //            //bucket[(datas[i] / exp) % BASE]++;
        //            int bx = (datas[i] / exp) % BASE;
        //            bucket[bx]++;

        //            // visualize C.
        //            DrawC(bx, bucket[bx] * 2);
        //            Thread.Sleep(_sleepTimespan);
        //        }

        //        for (int i = 1; i < BASE; i++)
        //        {
        //            bucket[i] += bucket[i - 1];

        //            // visualize C.
        //            DrawC(i, bucket[i]);
        //            Thread.Sleep(_sleepTimespan);
        //        }

        //        //# put to calculated index.
        //        ResetDataCanvas(); // for visualization
        //        for (int i = n - 1; i >= 0; i--)
        //        {
        //            // b[--bucket[(datas[i] / exp) % BASE]] = datas[i];
        //            int bx = (datas[i] / exp) % BASE;
        //            int bi = --bucket[bx];
        //            b[bi] = datas[i];

        //            // for visualization
        //            DrawC(bx, bucket[bx]); // 變化太微量，看不出變化
        //            DrawData(b, bi);
        //            Thread.Sleep(_sleepTimespan);
        //        }

        //        //
        //        CopyArray(b, 0, n - 1, datas);

        //        // next round.
        //        exp *= BASE;
        //    }
        //}

        #endregion of : backup

        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " - v" + Application.ProductVersion;
        }

        private void btnGenRand_Click(object sender, EventArgs e)
        {
            AspectW.Define
                .WaitCursor(this, btnGenRand)
                .OnEnter(()=> PrepareResource(this))
                .Do(() =>
                {
                    //# init. int array
                    _ADatas = AValueEx.GenRandDatas(0);

                    // draw datas first
                    AValueEx.DrawDatas(_ADatas, Pens.Blue);
                    AValueEx.ResetCountCanvas(); // reset C region.
                });
        }

        private void btnGenSorted_Click(object sender, EventArgs e)
        {
            AspectW.Define
                .WaitCursor(this, btnGenRand)
                .OnEnter(() => PrepareResource(this))
                .Do(() =>
                {
                    //# init. int array
                    _ADatas = AValueEx.GenRandDatas(1);

                    // draw datas first
                    AValueEx.DrawDatas(_ADatas, Pens.Blue);
                    AValueEx.ResetCountCanvas(); // reset C region.
                });
        }

        private void btnGenSortedR_Click(object sender, EventArgs e)
        {
            AspectW.Define
                .WaitCursor(this, btnGenRand)
                .OnEnter(() => PrepareResource(this))
                .Do(() =>
                {
                    //# init. int array
                    _ADatas = AValueEx.GenRandDatas(2);

                    // draw datas first
                    AValueEx.DrawDatas(_ADatas, Pens.Blue);
                    AValueEx.ResetCountCanvas(); // reset C region.
                });
        }

        private void btnBubbleSort_Click(object sender, EventArgs e)
        {
            AspectW.Define
                .WaitCursor(this, grpSorting)
                .OnEnterLeave(() => PrepareResource(this), () => ShowCount(this))
                .Do(() =>
                {
                    //# initialize datas
                    if (_ADatas == null)
                        _ADatas = AValueEx.GenRandDatas(0);

                    // duplicate and reset state 
                    AValue[] A = AValueEx.DuplicateDatas(_ADatas);

                    //# draw datas first - for visualization
                    AValueEx.DrawDatas(A, Pens.Blue);
                    AValueEx.ResetCountCanvas(); // reset C region.

                    Thread.Sleep(1000); // wait one second use to view source data state.

                    //# sorting ============
                    AutoResetEvent wait = new AutoResetEvent(false);
                    ThreadPool.QueueUserWorkItem(callback => { AValueEx.BubbleSort(A); wait.Set(); });
                    wait.WaitOne();
                });
        }

        private void btnSelectionSort_Click(object sender, EventArgs e)
        {
            AspectW.Define
                .WaitCursor(this, grpSorting)
                .OnEnterLeave(() => PrepareResource(this), () => ShowCount(this))
                .Do(() =>
                {
                    //# initialize datas
                    if (_ADatas == null)
                        _ADatas = AValueEx.GenRandDatas(0);

                    // duplicate and reset state 
                    AValue[] A = AValueEx.DuplicateDatas(_ADatas);

                    //# draw datas first - for visualization
                    AValueEx.DrawDatas(A, Pens.Blue);
                    AValueEx.ResetCountCanvas(); // reset C region.

                    Thread.Sleep(1000); // wait one second use to view source data state.

                    //# sorting ============
                    AutoResetEvent wait = new AutoResetEvent(false);
                    ThreadPool.QueueUserWorkItem(callback => { AValueEx.SelectionSort(A); wait.Set(); });
                    wait.WaitOne();
                 });
        }

        private void btnInsertionSort_Click(object sender, EventArgs e)
        {
            AspectW.Define
                .WaitCursor(this, grpSorting)
                .OnEnterLeave(() => PrepareResource(this), () => ShowCount(this))
                .Do(() =>
                {
                    //# initialize datas
                    if (_ADatas == null)
                        _ADatas = AValueEx.GenRandDatas(0);

                    // duplicate and reset state 
                    AValue[] A = AValueEx.DuplicateDatas(_ADatas);

                    //# draw datas first - for visualization
                    AValueEx.DrawDatas(A, Pens.Blue);
                    AValueEx.ResetCountCanvas(); // reset C region.

                    Thread.Sleep(1000); // wait one second use to view source data state.

                    //# sorting ============
                    AutoResetEvent wait = new AutoResetEvent(false);
                    ThreadPool.QueueUserWorkItem(callback => { AValueEx.InsertionSort(A); wait.Set(); });
                    wait.WaitOne();
                });
        }

        private void btnMergeSort_Click(object sender, EventArgs e)
        {
            AspectW.Define
                .WaitCursor(this, grpSorting)
                .OnEnterLeave(() => PrepareResource(this), () => ShowCount(this))
                .Do(() =>
                {
                    //# initialize datas
                    if (_ADatas == null)
                        _ADatas = AValueEx.GenRandDatas(0);

                    // duplicate and reset state 
                    AValue[] A = AValueEx.DuplicateDatas(_ADatas);

                    //# draw datas first - for visualization
                    AValueEx.DrawDatas(A, Pens.Blue);
                    AValueEx.ResetCountCanvas(); // reset C region.

                    Thread.Sleep(1000); // wait one second use to view source data state.

                    //# sorting ============
                    AutoResetEvent wait = new AutoResetEvent(false);
                    ThreadPool.QueueUserWorkItem(callback => { AValueEx.TopDownMergeSort(A); wait.Set(); });
                    wait.WaitOne();
                });
        }

        private void btnMergeSortB_Click(object sender, EventArgs e)
        {
            AspectW.Define
                .WaitCursor(this, grpSorting)
                .OnEnterLeave(() => PrepareResource(this), () => ShowCount(this))
                .Do(() =>
                {
                    //# initialize datas
                    if (_ADatas == null)
                        _ADatas = AValueEx.GenRandDatas(0);

                    // duplicate and reset state 
                    AValue[] A = AValueEx.DuplicateDatas(_ADatas);

                    //# draw datas first - for visualization
                    AValueEx.DrawDatas(A, Pens.Blue);
                    AValueEx.ResetCountCanvas(); // reset C region.

                    Thread.Sleep(1000); // wait one second use to view source data state.

                    //# sorting ============
                    AutoResetEvent wait = new AutoResetEvent(false);
                    ThreadPool.QueueUserWorkItem(callback => { AValueEx.BottomUpMergeSort(A); wait.Set(); });
                    wait.WaitOne();
                });
        }

        private void btnQuickSort_Click(object sender, EventArgs e)
        {
            AspectW.Define
                //.ReconfirmIntent()
                .WaitCursor(this, grpSorting)
                .OnEnterLeave(() => PrepareResource(this), () => ShowCount(this))
                .Do(() =>
                {
                    //# initialize datas
                    if (_ADatas == null)
                        _ADatas = AValueEx.GenRandDatas(0);

                    // duplicate and reset state 
                    AValue[] A = AValueEx.DuplicateDatas(_ADatas);

                    //# draw datas first - for visualization
                    AValueEx.DrawDatas(A, Pens.Blue);
                    AValueEx.ResetCountCanvas(); // reset C region.

                    Thread.Sleep(1000); // wait one second use to view source data state.

                    //# sorting ============
                    AutoResetEvent wait = new AutoResetEvent(false);
                    ThreadPool.QueueUserWorkItem(callback => { AValueEx.QuickSort(A, 0, A.Length-1); wait.Set(); });
                    wait.WaitOne();
                });
        }

        private void btnQuickSortRand_Click(object sender, EventArgs e)
        {
            AspectW.Define
                .WaitCursor(this, grpSorting)
                .OnEnterLeave(() => PrepareResource(this), () => ShowCount(this))
                .Do(() =>
                {
                    //# initialize datas
                    if (_ADatas == null)
                        _ADatas = AValueEx.GenRandDatas(0);

                    // duplicate and reset state 
                    AValue[] A = AValueEx.DuplicateDatas(_ADatas);

                    //# draw datas first - for visualization
                    AValueEx.DrawDatas(A, Pens.Blue);
                    AValueEx.ResetCountCanvas(); // reset C region.

                    Thread.Sleep(1000); // wait one second use to view source data state.

                    //# sorting ============
                    AutoResetEvent wait = new AutoResetEvent(false);
                    ThreadPool.QueueUserWorkItem(callback => { AValueEx.QuickSortRand(A, 0, A.Length-1); wait.Set(); });
                    wait.WaitOne();
                });
        }

        private void btnHeapSort_Click(object sender, EventArgs e)
        {
            AspectW.Define
                .WaitCursor(this, grpSorting)
                .OnEnterLeave(() => PrepareResource(this), () => ShowCount(this))
                .Do(() =>
                {
                    //# initialize datas
                    if (_ADatas == null)
                        _ADatas = AValueEx.GenRandDatas(0);

                    // duplicate and reset state 
                    AValue[] A = AValueEx.DuplicateDatas(_ADatas);

                    //# draw datas first - for visualization
                    AValueEx.DrawDatas(A, Pens.Blue);
                    AValueEx.ResetCountCanvas(); // reset C region.

                    Thread.Sleep(1000); // wait one second use to view source data state.

                    //# sorting ============
                    AutoResetEvent wait = new AutoResetEvent(false);
                    ThreadPool.QueueUserWorkItem(callback => { AValueEx.HeapSort(A); wait.Set(); });
                    wait.WaitOne();
                });
        }

        private void btnCountingSortV_Click(object sender, EventArgs e)
        {
            AspectW.Define
                .WaitCursor(this, grpSorting)
                .OnEnterLeave(() => PrepareResource(this), () => ShowCount(this))
                .Do(() =>
                {
                    //# initialize datas
                    if (_ADatas == null)
                        _ADatas = AValueEx.GenRandDatas(0);

                    // duplicate and reset state 
                    AValue[] A = AValueEx.DuplicateDatas(_ADatas);

                    //# draw datas first - for visualization
                    AValueEx.DrawDatas(A, Pens.Blue);
                    AValueEx.ResetCountCanvas(); // reset C region.

                    Thread.Sleep(1000); // wait one second use to view source data state.

                    //# sorting ============
                    AutoResetEvent wait = new AutoResetEvent(false);
                    ThreadPool.QueueUserWorkItem(callback => { AValueEx.CountingSort(A); wait.Set(); });
                    wait.WaitOne();
                });
        }

        private void btnRadixSortV_Click(object sender, EventArgs e)
        {
            AspectW.Define
                .WaitCursor(this, grpSorting)
                .OnEnterLeave(() => PrepareResource(this), () => ShowCount(this))
                .Do(() =>
                {
                    //# initialize datas
                    if (_ADatas == null)
                        _ADatas = AValueEx.GenRandDatas(0);

                    // duplicate and reset state 
                    AValue[] A = AValueEx.DuplicateDatas(_ADatas);

                    //# draw datas first - for visualization
                    AValueEx.DrawDatas(A, Pens.Blue);
                    AValueEx.ResetCountCanvas(); // reset C region.

                    Thread.Sleep(1000); // wait one second use to view source data state.

                    //# sorting ============
                    AutoResetEvent wait = new AutoResetEvent(false);
                    ThreadPool.QueueUserWorkItem(callback => { AValueEx.RadixSort(A); wait.Set(); });
                    wait.WaitOne();
                });
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }

    public class AValue
    {
        #region data members
        private int _value;
        private int _moveTimes;
        #endregion

        public AValue()
        {
            _value = 0;
            _moveTimes = 0;
        }

        public AValue(int value)
        {
            _value = value;
            _moveTimes = 0;
        }

        public int Value
        {
            get { return _value; }
        }

        public int MoveTimes
        {
            get { return _moveTimes; }
        }

        #region overriding operator

        static public bool operator > (AValue a, AValue b)
        {
            AValueEx._compareCount++; // counting
            return a._value > b._value;
        }

        static public bool operator >= (AValue a, AValue b)
        {
            AValueEx._compareCount++; // counting
            return a._value >= b._value;
        }

        static public bool operator < (AValue a, AValue b)
        {
            AValueEx._compareCount++; // counting
            return a._value < b._value;
        }

        static public bool operator <= (AValue a, AValue b)
        {
            AValueEx._compareCount++; // counting
            return a._value <= b._value;
        }

        #endregion

        public int IncreaseMoveTimes()
        {
            return ++_moveTimes;
        }

        public void ResetState()
        {
            _moveTimes = 0;
        }

    }

    static public class AValueEx // AValueExtensions
    {
        public static uint _exchangeCount = 0;
        public static uint _updateCount = 0;
        public static uint _compareCount = 0;
        public static uint _countingCount = 0;

        /// <summary>
        /// 基數排序法
        /// </summary>
        public static void RadixSort(AValue[] datas)
        {
            #region algorithm
            //#define MAX 20
            //#define SHOWPASS
            //#define BASE 10
            //void radixsort(int *a, int n)
            //{
            //  int i, b[MAX], m = a[0], exp = 1;
            //  for (i = 1; i < n; i++)
            //  {
            //    if (a[i] > m)
            //      m = a[i];
            //  }
            // 
            //  while (m / exp > 0)
            //  {
            //    int bucket[BASE] ={  0 };
            //    for (i = 0; i < n; i++)
            //      bucket[(a[i] / exp) % BASE]++;
            //    for (i = 1; i < BASE; i++)
            //      bucket[i] += bucket[i - 1];
            //    for (i = n - 1; i >= 0; i--)
            //      b[--bucket[(a[i] / exp) % BASE]] = a[i];
            //    for (i = 0; i < n; i++)
            //      a[i] = b[i];
            //    exp *= BASE;
            // 
            //    #ifdef SHOWPASS
            //      printf("\nPASS   : ");
            //      print(a, n);
            //    #endif
            //  }
            //}
            #endregion

            //## parameters
            const int BASE = 10;

            //## get max value
            int m = 99; // datas.Max();

            // with bucket sort
            int n = datas.Length;
            AValue[] B = new AValue[n]; // as sorted datas.
            int exp = 1;
            while (m / exp > 0)
            {
                //## bucket sort
                int[] bucket = new int[BASE];

                // visualize bucket.
                AValueEx.ResetCountCanvas();

                for (int i = 0; i < n; i++)
                {
                    //# bucket[(datas[i] / exp) % BASE]++;
                    int bx = (datas[i].Value / exp) % BASE; // bucket index.

                    //bucket[bx]++;
                    UpdateC(bucket, bx, bucket[bx] + 1);
                }

                for (int i = 1; i < BASE; i++)
                {
                    //bucket[i] += bucket[i-1];
                    UpdateC(bucket, i, bucket[i] + bucket[i-1]);
                }

                //# put to calculated index.
                AValueEx.ResetDataCanvas(); // for visualization

                for (int i = n - 1; i >= 0; i--)
                {
                    // b[--bucket[(datas[i] / exp) % BASE]] = datas[i];
                    int bx = (datas[i].Value / exp) % BASE;
                    int bi = --bucket[bx];
                    //B[bi] = datas[i];
                    Update(B, bi, datas[i]);

                    // for visualization
                    AValueEx.DrawC(bx, bucket[bx]); // 變化太微量，看不出變化
                }

                // copy back for next round sorting
                AValueEx.CopyArray(B, 0, n - 1, datas);

                // next round.
                exp *= BASE;
            }
        }

        /// <summary>
        /// 計數排序法
        /// </summary>
        public static void CountingSort(AValue[] datas)
        {
            #region algorithm
            //CountingSort(A,B,k)
            //1.for i <- 0 to k
            //2.  do C[i] <- 0
            //3.for j <- 1 to length[A]
            //4.  do C[A[j]] <- C[A[j]]+1
            //5. // C[i] now contains the number of elements equal to i.
            //6.for i <- 1 to k
            //7.  do C[i] <- C[i] + C[i-1]
            //8. // C[i] now contains the number of elements less than or equal to i.
            //9. for j <- length[A] downto 1
            //10.  do B[C[A[j]]] <- A[j]
            //11      C[A[j]] <- C[A[j]] - 1
            #endregion

            //# duplation as A in place of datas[]. and let datas[] as B;
            AValue[] A = new AValue[datas.Length];
            CopyArray(datas, 0, datas.Length -1, A);

            //## BEGIN to do counting sort.
            //# step 1,2.
            int[] C = new int[A.Length];
            for (int i = 0; i < C.Length; i++) // k == length[A], in this case.
                C[i] = 0; // do C[i] <- 0

            // show C - for visualizaton.
            AValueEx.ResetCountCanvas();

            //# step 3,4.
            for (int i = 0; i < A.Length; i++)
            {
                // C[A[i]]++;
                int ci = A[i].Value; // C index
                UpdateC(C, ci, C[ci]+1);
            }

            //# step 6,7.
            for (int i = 1; i < C.Length; i++)
            {
                // C[i] = C[i] + C[i-1];
                UpdateC(C, i, C[i] + C[i - 1]);
            }

            //# step 8,9,10.
            AValueEx.ResetDataCanvas(); // for visualization

            for (int i = A.Length - 1; i >= 0; i--)
            {
                //# B[C[A[j]]] <- A[j]
                // 'datas' as B
                int di = C[A[i].Value] - 1; // zero base array.          
                Update(datas, di, A[i]); // //datas[di] = A[i];

                //# C[A[j]] <- C[A[j]] - 1
                int ci = A[i].Value; 
                C[ci]--;

                // visualize C
                AValueEx.DrawC(ci, C[ci]); // 變化太微量，看不出變化
            }

        }

        /// <summary>
        /// 堆積排序法
        /// </summary>
        public static void HeapSort(AValue[] datas)
        {
            #region algorithm
            // Heapsort(A)
            //1  Build-Max-Heap(A)
            //2  for i <- length[A] down to 2
            //3     do exchange A[1]<->A[i]  --- 1 base array
            //4          heap-size[A] <- heap-size[A] -1
            //5          Max-Heapify(A,1)
            #endregion

            BuildMaxHeap(datas);

            int heap_size = datas.Length;
            for (int i = datas.Length - 1; i >= 1; i--)
            {
                Exchange(datas, i, 0); // 0 base array
                heap_size--;
                MaxHeapify(datas, heap_size, 0);
            }
        }

        public static void BuildMaxHeap(AValue[] datas)
        {
            #region algorithm
            //Build-Max-Heap(A)
            //1   heap-size[A] <- length[A]
            //2   for i <- floor(length[A]/2) downto 1 --- 1 base array
            //3  	    do Max-Heapify(A, i)
            #endregion

            int heap_size = datas.Length;
            for (int i = (datas.Length - 1) / 2; i >= 0; i--)  //--- 0 base array
                MaxHeapify(datas, heap_size, i);
        }

        public static void MaxHeapify(AValue[] datas, int heap_size, int index)
        {
            #region algorithm
            // Max-Heapify (A, i )
            //1 l <- Left (i )
            //2 r <- Right(i )
            //3 if l ≤ heap-size[A] and A[l] > A[i ]
            //4 		then largest <- l
            //5 		else largest <- i
            //6 if r ≤ heap-size[A] and A[r] > A[largest]
            //7		then largest <- r
            //8 if largest <> i
            //9  		then exchange A[i] <-> A[largest]
            //10 		     Max-Heapify (A, largest)
            #endregion

            //int heap_size = datas.Length;
            int l = HeapLeft(index);
            int r = HeapRight(index);

            int largest = index;
            if (l < heap_size && datas[l] > datas[largest])
                largest = l;
            if (r < heap_size && datas[r] > datas[largest])
                largest = r;

            if (largest != index)
            {
                Exchange(datas, index, largest); // exchange A[i] <-> A[largest] 
                MaxHeapify(datas, heap_size, largest);
            }
        }

        public static int HeapLeft(int index)
        {
            //Left child of A[i] = A[2 * i]. --- 1 base
            return ((index + 1) * 2 - 1); // --- 0 base
        }

        public static int HeapRight(int index)
        {
            //Right child of A[i] = A[2 * i + 1].
            return HeapLeft(index) + 1;
        }

        /// <summary>
        /// 快速排序法
        /// </summary>
        public static void QuickSort(AValue[] datas, int p, int r)
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

        public static int QuickSortPartition(AValue[] datas, int p, int r)
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

            AValue x = datas[r];
            int i = p - 1;
            for (int j = p; j < r; j++)
                if (datas[j] < x)
                {
                    // i = i + 1;
                    i++;
                    // exchange A[i]<->A[j]
                    Exchange(datas, i, j);
                }

            // exchange A[i+1] <-> A[r]
            i++;
            if(i != r) 
                Exchange(datas, i, r);

            return i;
        }

        public static void QuickSortRand(AValue[] datas, int p, int r)
        {
            #region algorithm
            //RANDOMIZED_QUICKSORT(A,p,r)
            //1  if p < r then
            //2     q <- RANDOMIZED_PARTITION(A,p,r)
            //3     RANDOMIZED_QUICKSORT(A, p, q-1)
            //4     RANDOMIZED_QUICKSORT(A,q+1,r)
            #endregion

            if (p < r)
            {
                int q = QuickSortRandPartition(datas, p, r);
                QuickSortRand(datas, p, q - 1);
                QuickSortRand(datas, q + 1, r);
            }
        }

        public static int QuickSortRandPartition(AValue[] datas, int p, int r)
        {
            #region algorithm
            //RANDOMIZED_PARTITION(A,p,r)
            //1   i <- RANDOM(p,r)
            //2   exchange A[r]<->A[i]
            //3   return PARTITION(A,p,r)
            #endregion

            // randomize
            Random rand = new Random();
            int i = p + rand.Next(r - p);
            Exchange(datas, r, i); // exchange A[r]<->A[i]
            return QuickSortPartition(datas, p, r);
        }

        /// <summary>
        /// 合併排序法
        /// </summary>
        public static void TopDownMergeSort(AValue[] datas)
        {
            #region algorithm
            //TopDownMergeSort(A[], B[], n)
            //{
            //    TopDownSplitMerge(A, 0, n, B);
            //}
            #endregion

            AValue[] mergedDatas = new AValue[datas.Length];
            TopDownSplitMerge(datas, 0, datas.Length - 1, mergedDatas);
        }

        public static void TopDownSplitMerge(AValue[] datas, int iBegin, int iEnd, AValue[] mergedDatas)
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

            //Debug.WriteLine(string.Format("TopDownSplitMerge iBegin ~ iEnd : {0} ~ {1}", iBegin, iEnd));

            if (iEnd <= iBegin) // consider it sorted.
                return;

            int iMiddle = (iEnd + iBegin) / 2;
            TopDownSplitMerge(datas, iBegin, iMiddle, mergedDatas); // split / merge left  half
            TopDownSplitMerge(datas, iMiddle + 1, iEnd, mergedDatas); // split / merge right half
            Merge(datas, iBegin, iMiddle, iEnd, mergedDatas);
            CopyArray(mergedDatas, iBegin, iEnd, datas); // // copy the merged runs back to A
        }

        public static void Merge(AValue[] A, int iBegin, int iMiddle, int iEnd, AValue[] B)
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

            //# before
            ResetDataCanvas(iBegin, 0, iEnd, 99); // reset data canvas - for visualization
            DrawC(iBegin, iEnd, Pens.Green, true); // draw C canvas - for visualization

            //# do merge
            int i0 = iBegin;
            int i1 = iMiddle + 1;
            int j = iBegin;
            while (i0 <= iMiddle && i1 <= iEnd)
            {
                if (A[i0] <= A[i1])
                {
                    DrawC(i0, Pens.LightGray); // reset C - for visualization
                    // B[j++] = A[i0++];
                    Update(B, j++, A[i0++]);
                }
                else
                {
                    DrawC(i1, Pens.LightGray); // reset C - for visualization
                    // B[j++] = A[i1++];
                    Update(B, j++, A[i1++]);
                }
            }

            while (i0 <= iMiddle)
            {
                DrawC(i0, Pens.LightGray); // reset C - for visualization.
                //B[j++] = A[i0++];
                Update(B, j++, A[i0++]);
            }

            while (i1 <= iEnd)
            {
                DrawC(i1, Pens.LightGray); // reset C - for visualization.
                //B[j++] = A[i1++];
                Update(B, j++, A[i1++]);
            }
        }

        /// <summary>
        /// 下上合併排序法
        /// </summary>
        /// <param name="datas"></param>
        public static void BottomUpMergeSort(AValue[] datas)
        {
            /* let 'datas' as array A */
            /* let 'mergedDatas' as array B*/
            AValue[] mergedDatas = new AValue[datas.Length];

            /* Each 1-element run in A is already "sorted". */
            /* Make successively longer sorted runs of length 2, 4, 8, 16... until whole array is sorted. */
            int n = datas.Length - 1;
            for (int width = 1; width <= n; width = 2 * width)
            {
                /* Array A is full of runs of length width. */
                for (int i = 0; i <= n; i = i + 2 * width)
                {
                    /* Merge two runs: A[i:i+width-1] and A[i+width:i+2*width-1] to B[] */
                    /* or copy A[i:n-1] to B[] ( if(i+width >= n) ) */
                    Merge(datas, i, Min(i + width - 1, n), Min(i + 2 * width - 1, n), mergedDatas);
                }

                /* Now work array B is full of runs of length 2*width. */
                /* Copy array B to array A for next iteration. */
                /* A more efficient implementation would swap the roles of A and B */
                CopyArray(mergedDatas, 0, n, datas);

                /* Now array A is full of runs of length 2*width. */
            }
        }

        /// <summary>
        /// 插入排序法
        /// </summary>
        public static void InsertionSort(AValue[] datas)
        {
            //從未排序數列取出一元素。
            //由後往前和已排序數列元素比較，直到遇到不大於自己的元素並插入此元素之後；若都沒有則插入在最前面。
            //重複以上動作直到未排序數列全部處理完成。

            AValue[] sortedDatas = new AValue[datas.Length];
            for (int i = 0; i < datas.Length; i++)
            {
                // 取出
                AValue c = datas[i];

                // 比較
                int insertIndex = i;
                for (; insertIndex > 0 && sortedDatas[insertIndex - 1] > c; insertIndex--) ; // compare

                // 插入-shift
                for (int j = i; j > insertIndex; j--)
                {
                    Update(sortedDatas, j, sortedDatas[j-1]);
                }

                // 插入-new
                Update(sortedDatas, insertIndex, c, Pens.Red);
            }

            // copy back
            CopyArray(sortedDatas, 0, sortedDatas.Length - 1, datas);
        }

        /// <summary>
        /// 泡沫排序法
        /// </summary>
        public static void BubbleSort(AValue[] datas)
        {
            for (int i = 0; i < datas.Length - 1; i++)
                for (int j = i + 1; j < datas.Length; j++)
                    if (datas[i] > datas[j]) // compare
                        Exchange(datas, i, j); // swap
        }

        /// <summary>
        /// 選擇排序法
        /// </summary>
        public static void SelectionSort(AValue[] datas)
        {
            #region algorithm
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
            #endregion

            //# Index i, j, max  --- or min
            int minIndex;
            for (int i = 0; i < datas.Length; i++)
            {
                minIndex = i;
                for (int j = i + 1; j < datas.Length; j++)
                    if (datas[j] < datas[minIndex]) // compare
                        minIndex = j;

                //# Exchange data[i] and data[min]
                if(i != minIndex)
                    Exchange(datas, i, minIndex);
            }
        }

        /* ====== ACTION ====== */

        public static void Exchange(AValue[] datas, int i, int j)
        {
            // exchange 
            AValue tmp = datas[i];
            datas[i] = datas[j];
            datas[i].IncreaseMoveTimes();
            datas[j] = tmp;
            datas[j].IncreaseMoveTimes();

            // visual exchange
            DrawData(datas, i);
            DrawData(datas, j);

            // counting
            _exchangeCount++;

            // wait a short time
            Thread.Sleep(Form1._sleepTimespan);
        }

        public static void Update(AValue[] datas, int index, AValue value)
        {
            // update value
            datas[index] = value;
            datas[index].IncreaseMoveTimes();

            // visual update
            DrawData(datas, index);

            // counting
            _updateCount++;

            // wait a short time
            Thread.Sleep(Form1._sleepTimespan);
        }

        public static void Update(AValue[] datas, int index, AValue value, Pen pen)
        {
            // update value
            datas[index] = value;
            datas[index].IncreaseMoveTimes();

            // visual update
            DrawData(datas, index, true, pen);

            // counting
            _updateCount++;

            // wait a short time
            Thread.Sleep(Form1._sleepTimespan);
        }

        public static void UpdateC(int[] C, int index, int value)
        {
            // update value
            C[index] = value;

            // visual update
            DrawC(index, value);

            // counting
            _countingCount++;

            // wait a short time
            Thread.Sleep(Form1._sleepTimespan);
        }

        /* ====== HELPER ====== */

        public static void WaitAStep()
        {
            Thread.Sleep(Form1._sleepTimespan);
        }

        public static int Min(int a, int b)
        {
            return a > b ? b : a;
        }

        public static void DrawC(int x, Pen pen)
        {
            Form1._g.DrawLine(pen, x, -5, x, -4);
        }

        public static void DrawC(int x, int v)
        {
            Pen pen = new Pen(MapColor(v));
            DrawC(x, pen);
        }

        public static void DrawC(int x1, int x2, Pen pen, bool redrawBackground)
        {
            if(redrawBackground)
                Form1.ResetDataCanvas(0, -5, 99, -4);
 
            Form1.DrawDataCanvas(pen.Color, x1, -5, x2, -4);
        }

        public static void DrawData(AValue[] datas, int index, bool redrawBackground, Pen pen)
        {
            // redraw background
            if (redrawBackground)
                Form1._g.DrawLine(Pens.LightGray, index, 99, index, 0);

            // draw data
            Form1._g.DrawLine(pen, index, datas[index].Value, index, 0);
        }

        public static void DrawData(AValue[] datas, int index, bool redrawBackground)
        {
            Pen pen = new Pen(MapColor(datas[index].MoveTimes));
            DrawData(datas, index, redrawBackground, pen);
        }

        public static void DrawData(AValue[] datas, int index)
        {
            Pen pen = new Pen(MapColor(datas[index].MoveTimes));
            DrawData(datas, index, true, pen);
        }

        public static void DrawDatas(AValue[] datas, Pen pen)
        {
            // reset data canvas
            ResetDataCanvas(0, 0, 99, 99);

            // draw one by one
            for (int i = 0; i < datas.Length; i++)
                Form1._g.DrawLine(pen, i, datas[i].Value, i, 0);
        }

        public static void ResetDataCanvas(int x1, int y1, int x2, int y2)
        {
            // reset data canvas
            Form1.ResetDataCanvas(x1, y1, x2, y2);
        }

        public static void ResetDataCanvas()
        {
            Form1.ResetDataCanvas(0, 0, 99, 99);
        }

        public static void ResetCountCanvas()
        {
            Form1.ResetDataCanvas(0, -5, 99, -4);
        }

        /// <summary>
        /// 產生亂數數列或排序、反排序數列。
        /// </summary>
        /// <param name="mode">0.randoom; 1.sorted; 2.reverse sorted</param>
        /// <returns></returns>
        public static AValue[] GenRandDatas(int mode)
        {
            const int size = 100;

            //# init. int array
            AValue[] datas = new AValue[size];
            for (int i = 0; i < size; i++)
                datas[i] = new AValue(mode == 2 ? size - 1 - i : i); // 0 ~ 99

            if (mode == 1 || mode == 2)
                return datas;

            // randomize
            Random r = new Random();
            for (int i = 0; i < size; i++)
            {
                int i1 = r.Next(size);
                int i2 = r.Next(size);
                // swap
                AValue tmp = datas[i1];
                datas[i1] = datas[i2];
                datas[i2] = tmp;
            }

            return datas;
        }

        /// <summary>
        /// 依值取得 Color 階層，值越大顏色越深。
        /// </summary>
        public static Color MapColor(int value)
        {
            //{Name=Pink, ARGB=(255, 255, 192, 203)}  
            //{Name=Cyan, ARGB=(255, 0, 255, 255)}
            //{Name=Orange, ARGB=(255, 255, 165, 0)}
            //{Name=DarkBlue, ARGB=(255, 0, 0, 139)}
            //{Name=Brown, ARGB=(255, 165, 42, 42)}

            int nr, ng, nb;
            int dr, dg, db;
            if (value <= 10)
            {
                nr = value;
                ng = value;
                nb = value;
                dr = 10;
                dg = 0;
                db = 10;
                //return Color.FromArgb(255 - dr * nr, 192 - dg * ng, 203 - db * nb);
                return Color.FromArgb(155 + dr * nr, 192 - dg * ng, 103 + db * nb);
            }

            if (value <= 100)
            {
                nr = 0;
                ng = value;
                nb = value;
                dr = 0;
                dg = 2;
                db = 1;
                return Color.FromArgb(0 - dr * nr, 255 - dg * ng, 255 - db * nb);
            }

            //{Name=Orange, ARGB=(255, 255, 165, 0)}
            nr = value / 3;
            ng = value / 9;
            nb = value / 27;
            dr = 2;
            dg = 1;
            db = 1;
            return Color.FromArgb(165 - dr * nr, 42 - dg * ng, 42 - db * nb);

            //return Color.Black;

            //int v1 = value % 100;
            //int v2 = value / 100;
            //return Color.FromArgb(240 - value * 2, 128, 240 - value * 2); // r,g,b, 值越大顏色越重
            
            //return _MapColor2(value);
        }

        public static void CopyArray(AValue[] dataFrom, int iBegin, int iEnd, AValue[] dataTo)
        {
            for (int i = iBegin; i <= iEnd; i++)
                dataTo[i] = dataFrom[i];
        }

        public static void ResetDatasState(AValue[] datas)
        {
            for (int i = 0; i < datas.Length; i++)
                datas[i].ResetState();

            // reset state
            _compareCount = 0;
            _exchangeCount = 0;
            _updateCount = 0;
            _countingCount = 0;
        }

        /// <summary>
        /// 複製數據列並重製狀態
        /// </summary>
        public static AValue[] DuplicateDatas(AValue[] datas)
        {
            AValue[] A = new AValue[datas.Length];
            for (int i = 0; i < datas.Length; i++)
                A[i] = new AValue(datas[i].Value); // new AValue state is reset.
          
            // reset state
            _compareCount = 0;
            _exchangeCount = 0;
            _updateCount = 0;
            _countingCount = 0;

            return A;
        }

    }

}
