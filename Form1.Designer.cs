namespace winFrm_SortLab
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.numSleepTimespan = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCompare = new System.Windows.Forms.Label();
            this.lblExchange = new System.Windows.Forms.Label();
            this.btnGenSortedR = new System.Windows.Forms.Button();
            this.btnGenSorted = new System.Windows.Forms.Button();
            this.btnGenRand = new System.Windows.Forms.Button();
            this.grpSorting = new System.Windows.Forms.GroupBox();
            this.btnRadixSortV = new System.Windows.Forms.Button();
            this.btnCountingSortV = new System.Windows.Forms.Button();
            this.btnHeapSort = new System.Windows.Forms.Button();
            this.btnQuickSortRand = new System.Windows.Forms.Button();
            this.btnQuickSort = new System.Windows.Forms.Button();
            this.btnMergeSortB = new System.Windows.Forms.Button();
            this.btnMergeSort = new System.Windows.Forms.Button();
            this.btnInsertionSort = new System.Windows.Forms.Button();
            this.btnSelectionSort = new System.Windows.Forms.Button();
            this.btnBubbleSort = new System.Windows.Forms.Button();
            this.lblUpdate = new System.Windows.Forms.Label();
            this.lblCounting = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chkAsyncMode = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numSleepTimespan)).BeginInit();
            this.grpSorting.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(12, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(449, 446);
            this.panel1.TabIndex = 1;
            // 
            // numSleepTimespan
            // 
            this.numSleepTimespan.Location = new System.Drawing.Point(83, 7);
            this.numSleepTimespan.Name = "numSleepTimespan";
            this.numSleepTimespan.Size = new System.Drawing.Size(53, 22);
            this.numSleepTimespan.TabIndex = 3;
            this.numSleepTimespan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numSleepTimespan.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "延遲毫秒：";
            // 
            // lblCompare
            // 
            this.lblCompare.AutoSize = true;
            this.lblCompare.Location = new System.Drawing.Point(485, 328);
            this.lblCompare.Name = "lblCompare";
            this.lblCompare.Size = new System.Drawing.Size(101, 12);
            this.lblCompare.TabIndex = 6;
            this.lblCompare.Text = "比較次數：？？？";
            // 
            // lblExchange
            // 
            this.lblExchange.AutoSize = true;
            this.lblExchange.Location = new System.Drawing.Point(485, 348);
            this.lblExchange.Name = "lblExchange";
            this.lblExchange.Size = new System.Drawing.Size(101, 12);
            this.lblExchange.TabIndex = 7;
            this.lblExchange.Text = "交換次數：？？？";
            // 
            // btnGenSortedR
            // 
            this.btnGenSortedR.Location = new System.Drawing.Point(357, 9);
            this.btnGenSortedR.Name = "btnGenSortedR";
            this.btnGenSortedR.Size = new System.Drawing.Size(97, 23);
            this.btnGenSortedR.TabIndex = 2;
            this.btnGenSortedR.Text = "反排序數列";
            this.btnGenSortedR.UseVisualStyleBackColor = true;
            this.btnGenSortedR.Click += new System.EventHandler(this.btnGenSortedR_Click);
            // 
            // btnGenSorted
            // 
            this.btnGenSorted.Location = new System.Drawing.Point(254, 9);
            this.btnGenSorted.Name = "btnGenSorted";
            this.btnGenSorted.Size = new System.Drawing.Size(97, 23);
            this.btnGenSorted.TabIndex = 1;
            this.btnGenSorted.Text = "已排序數列";
            this.btnGenSorted.UseVisualStyleBackColor = true;
            this.btnGenSorted.Click += new System.EventHandler(this.btnGenSorted_Click);
            // 
            // btnGenRand
            // 
            this.btnGenRand.Location = new System.Drawing.Point(151, 9);
            this.btnGenRand.Name = "btnGenRand";
            this.btnGenRand.Size = new System.Drawing.Size(97, 23);
            this.btnGenRand.TabIndex = 0;
            this.btnGenRand.Text = "產生亂數數列";
            this.btnGenRand.UseVisualStyleBackColor = true;
            this.btnGenRand.Click += new System.EventHandler(this.btnGenRand_Click);
            // 
            // grpSorting
            // 
            this.grpSorting.Controls.Add(this.btnRadixSortV);
            this.grpSorting.Controls.Add(this.btnCountingSortV);
            this.grpSorting.Controls.Add(this.btnHeapSort);
            this.grpSorting.Controls.Add(this.btnQuickSortRand);
            this.grpSorting.Controls.Add(this.btnQuickSort);
            this.grpSorting.Controls.Add(this.btnMergeSortB);
            this.grpSorting.Controls.Add(this.btnMergeSort);
            this.grpSorting.Controls.Add(this.btnInsertionSort);
            this.grpSorting.Controls.Add(this.btnSelectionSort);
            this.grpSorting.Controls.Add(this.btnBubbleSort);
            this.grpSorting.Location = new System.Drawing.Point(467, 7);
            this.grpSorting.Name = "grpSorting";
            this.grpSorting.Size = new System.Drawing.Size(136, 310);
            this.grpSorting.TabIndex = 13;
            this.grpSorting.TabStop = false;
            this.grpSorting.Text = "排序";
            // 
            // btnRadixSortV
            // 
            this.btnRadixSortV.Location = new System.Drawing.Point(12, 280);
            this.btnRadixSortV.Name = "btnRadixSortV";
            this.btnRadixSortV.Size = new System.Drawing.Size(111, 23);
            this.btnRadixSortV.TabIndex = 19;
            this.btnRadixSortV.Text = "J基數排序法";
            this.btnRadixSortV.UseVisualStyleBackColor = true;
            this.btnRadixSortV.Click += new System.EventHandler(this.btnRadixSortV_Click);
            // 
            // btnCountingSortV
            // 
            this.btnCountingSortV.Location = new System.Drawing.Point(12, 251);
            this.btnCountingSortV.Name = "btnCountingSortV";
            this.btnCountingSortV.Size = new System.Drawing.Size(111, 23);
            this.btnCountingSortV.TabIndex = 18;
            this.btnCountingSortV.Text = "I計數排序法";
            this.btnCountingSortV.UseVisualStyleBackColor = true;
            this.btnCountingSortV.Click += new System.EventHandler(this.btnCountingSortV_Click);
            // 
            // btnHeapSort
            // 
            this.btnHeapSort.Location = new System.Drawing.Point(12, 222);
            this.btnHeapSort.Name = "btnHeapSort";
            this.btnHeapSort.Size = new System.Drawing.Size(111, 23);
            this.btnHeapSort.TabIndex = 17;
            this.btnHeapSort.Text = "H堆積排序法";
            this.btnHeapSort.UseVisualStyleBackColor = true;
            this.btnHeapSort.Click += new System.EventHandler(this.btnHeapSort_Click);
            // 
            // btnQuickSortRand
            // 
            this.btnQuickSortRand.Location = new System.Drawing.Point(12, 135);
            this.btnQuickSortRand.Name = "btnQuickSortRand";
            this.btnQuickSortRand.Size = new System.Drawing.Size(111, 23);
            this.btnQuickSortRand.TabIndex = 16;
            this.btnQuickSortRand.Text = "E亂數快速排序";
            this.btnQuickSortRand.UseVisualStyleBackColor = true;
            this.btnQuickSortRand.Click += new System.EventHandler(this.btnQuickSortRand_Click);
            // 
            // btnQuickSort
            // 
            this.btnQuickSort.Location = new System.Drawing.Point(12, 108);
            this.btnQuickSort.Name = "btnQuickSort";
            this.btnQuickSort.Size = new System.Drawing.Size(111, 23);
            this.btnQuickSort.TabIndex = 15;
            this.btnQuickSort.Text = "D快速排序法";
            this.btnQuickSort.UseVisualStyleBackColor = true;
            this.btnQuickSort.Click += new System.EventHandler(this.btnQuickSort_Click);
            // 
            // btnMergeSortB
            // 
            this.btnMergeSortB.Location = new System.Drawing.Point(12, 193);
            this.btnMergeSortB.Name = "btnMergeSortB";
            this.btnMergeSortB.Size = new System.Drawing.Size(111, 23);
            this.btnMergeSortB.TabIndex = 14;
            this.btnMergeSortB.Text = "G下上合併排序";
            this.btnMergeSortB.UseVisualStyleBackColor = true;
            this.btnMergeSortB.Click += new System.EventHandler(this.btnMergeSortB_Click);
            // 
            // btnMergeSort
            // 
            this.btnMergeSort.Location = new System.Drawing.Point(12, 164);
            this.btnMergeSort.Name = "btnMergeSort";
            this.btnMergeSort.Size = new System.Drawing.Size(111, 23);
            this.btnMergeSort.TabIndex = 13;
            this.btnMergeSort.Text = "F合併排序法";
            this.btnMergeSort.UseVisualStyleBackColor = true;
            this.btnMergeSort.Click += new System.EventHandler(this.btnMergeSort_Click);
            // 
            // btnInsertionSort
            // 
            this.btnInsertionSort.Location = new System.Drawing.Point(12, 21);
            this.btnInsertionSort.Name = "btnInsertionSort";
            this.btnInsertionSort.Size = new System.Drawing.Size(111, 23);
            this.btnInsertionSort.TabIndex = 12;
            this.btnInsertionSort.Text = "A插入排序法";
            this.btnInsertionSort.UseVisualStyleBackColor = true;
            this.btnInsertionSort.Click += new System.EventHandler(this.btnInsertionSort_Click);
            // 
            // btnSelectionSort
            // 
            this.btnSelectionSort.Location = new System.Drawing.Point(12, 79);
            this.btnSelectionSort.Name = "btnSelectionSort";
            this.btnSelectionSort.Size = new System.Drawing.Size(111, 23);
            this.btnSelectionSort.TabIndex = 11;
            this.btnSelectionSort.Text = "C選擇排序法";
            this.btnSelectionSort.UseVisualStyleBackColor = true;
            this.btnSelectionSort.Click += new System.EventHandler(this.btnSelectionSort_Click);
            // 
            // btnBubbleSort
            // 
            this.btnBubbleSort.Location = new System.Drawing.Point(12, 50);
            this.btnBubbleSort.Name = "btnBubbleSort";
            this.btnBubbleSort.Size = new System.Drawing.Size(111, 23);
            this.btnBubbleSort.TabIndex = 10;
            this.btnBubbleSort.Text = "B泡沫排序法";
            this.btnBubbleSort.UseVisualStyleBackColor = true;
            this.btnBubbleSort.Click += new System.EventHandler(this.btnBubbleSort_Click);
            // 
            // lblUpdate
            // 
            this.lblUpdate.AutoSize = true;
            this.lblUpdate.Location = new System.Drawing.Point(485, 368);
            this.lblUpdate.Name = "lblUpdate";
            this.lblUpdate.Size = new System.Drawing.Size(101, 12);
            this.lblUpdate.TabIndex = 14;
            this.lblUpdate.Text = "移動次數：？？？";
            // 
            // lblCounting
            // 
            this.lblCounting.AutoSize = true;
            this.lblCounting.Location = new System.Drawing.Point(485, 388);
            this.lblCounting.Name = "lblCounting";
            this.lblCounting.Size = new System.Drawing.Size(101, 12);
            this.lblCounting.TabIndex = 15;
            this.lblCounting.Text = "定址計算：？？？";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Maroon;
            this.label2.Location = new System.Drawing.Point(473, 449);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "※比較運算不延遲。";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Maroon;
            this.label3.Location = new System.Drawing.Point(473, 431);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "※重點在資料搬移過程。";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(485, 408);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(101, 12);
            this.lblTotal.TabIndex = 18;
            this.lblTotal.Text = "次數總計：？？？";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Maroon;
            this.label4.Location = new System.Drawing.Point(473, 467);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "※實際花費時間不精準。";
            // 
            // chkAsyncMode
            // 
            this.chkAsyncMode.AutoSize = true;
            this.chkAsyncMode.Location = new System.Drawing.Point(59, 32);
            this.chkAsyncMode.Name = "chkAsyncMode";
            this.chkAsyncMode.Size = new System.Drawing.Size(84, 16);
            this.chkAsyncMode.TabIndex = 0;
            this.chkAsyncMode.Text = "非同步模式";
            this.toolTip1.SetToolTip(this.chkAsyncMode, "當會變成「無法回應」狀態時再啟用。");
            this.chkAsyncMode.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 502);
            this.Controls.Add(this.chkAsyncMode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCounting);
            this.Controls.Add(this.lblUpdate);
            this.Controls.Add(this.grpSorting);
            this.Controls.Add(this.btnGenRand);
            this.Controls.Add(this.btnGenSorted);
            this.Controls.Add(this.btnGenSortedR);
            this.Controls.Add(this.lblExchange);
            this.Controls.Add(this.lblCompare);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numSleepTimespan);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Sorting Visualization Lab";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numSleepTimespan)).EndInit();
            this.grpSorting.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown numSleepTimespan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCompare;
        private System.Windows.Forms.Label lblExchange;
        private System.Windows.Forms.Button btnGenSortedR;
        private System.Windows.Forms.Button btnGenSorted;
        private System.Windows.Forms.Button btnGenRand;
        private System.Windows.Forms.GroupBox grpSorting;
        private System.Windows.Forms.Button btnBubbleSort;
        private System.Windows.Forms.Button btnSelectionSort;
        private System.Windows.Forms.Button btnInsertionSort;
        private System.Windows.Forms.Label lblUpdate;
        private System.Windows.Forms.Button btnMergeSort;
        private System.Windows.Forms.Button btnMergeSortB;
        private System.Windows.Forms.Button btnQuickSort;
        private System.Windows.Forms.Button btnQuickSortRand;
        private System.Windows.Forms.Button btnHeapSort;
        private System.Windows.Forms.Button btnCountingSortV;
        private System.Windows.Forms.Button btnRadixSortV;
        private System.Windows.Forms.Label lblCounting;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkAsyncMode;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

