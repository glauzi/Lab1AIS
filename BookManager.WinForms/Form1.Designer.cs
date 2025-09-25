namespace BookManager.WinForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridViewBooks = new DataGridView();
            groupBoxEdit = new GroupBox();
            buttonDelete = new Button();
            buttonUpdate = new Button();
            buttonAdd = new Button();
            textBoxYear = new TextBox();
            label4 = new Label();
            textBoxGenre = new TextBox();
            label3 = new Label();
            textBoxAuthor = new TextBox();
            label2 = new Label();
            textBoxTitle = new TextBox();
            label1 = new Label();
            groupBoxFunctions = new GroupBox();
            listBoxYearResults = new ListBox();
            buttonFindByYear = new Button();
            textBoxYearFilter = new TextBox();
            label5 = new Label();
            listBoxGenres = new ListBox();
            buttonGroupByGenre = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewBooks).BeginInit();
            groupBoxEdit.SuspendLayout();
            groupBoxFunctions.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridViewBooks
            // 
            dataGridViewBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewBooks.Dock = DockStyle.Top;
            dataGridViewBooks.Location = new Point(0, 0);
            dataGridViewBooks.Name = "dataGridViewBooks";
            dataGridViewBooks.Size = new Size(784, 235);
            dataGridViewBooks.TabIndex = 0;
            dataGridViewBooks.SelectionChanged += dataGridViewBooks_SelectionChanged;
            // 
            // groupBoxEdit
            // 
            groupBoxEdit.Controls.Add(buttonDelete);
            groupBoxEdit.Controls.Add(buttonUpdate);
            groupBoxEdit.Controls.Add(buttonAdd);
            groupBoxEdit.Controls.Add(textBoxYear);
            groupBoxEdit.Controls.Add(label4);
            groupBoxEdit.Controls.Add(textBoxGenre);
            groupBoxEdit.Controls.Add(label3);
            groupBoxEdit.Controls.Add(textBoxAuthor);
            groupBoxEdit.Controls.Add(label2);
            groupBoxEdit.Controls.Add(textBoxTitle);
            groupBoxEdit.Controls.Add(label1);
            groupBoxEdit.Location = new Point(10, 260);
            groupBoxEdit.Name = "groupBoxEdit";
            groupBoxEdit.Size = new Size(774, 139);
            groupBoxEdit.TabIndex = 1;
            groupBoxEdit.TabStop = false;
            groupBoxEdit.Text = "Управление книгами";
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(200, 100);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(75, 23);
            buttonDelete.TabIndex = 10;
            buttonDelete.Text = "Удалить";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // buttonUpdate
            // 
            buttonUpdate.Location = new Point(105, 100);
            buttonUpdate.Name = "buttonUpdate";
            buttonUpdate.Size = new Size(75, 23);
            buttonUpdate.TabIndex = 9;
            buttonUpdate.Text = "Обновить";
            buttonUpdate.UseVisualStyleBackColor = true;
            buttonUpdate.Click += buttonUpdate_Click;
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(10, 100);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(75, 23);
            buttonAdd.TabIndex = 8;
            buttonAdd.Text = "Добавить";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // textBoxYear
            // 
            textBoxYear.Location = new Point(370, 58);
            textBoxYear.Name = "textBoxYear";
            textBoxYear.Size = new Size(100, 23);
            textBoxYear.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(332, 63);
            label4.Name = "label4";
            label4.Size = new Size(26, 15);
            label4.TabIndex = 6;
            label4.Text = "Год";
            // 
            // textBoxGenre
            // 
            textBoxGenre.Location = new Point(370, 22);
            textBoxGenre.Name = "textBoxGenre";
            textBoxGenre.Size = new Size(200, 23);
            textBoxGenre.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(326, 25);
            label3.Name = "label3";
            label3.Size = new Size(38, 15);
            label3.TabIndex = 4;
            label3.Text = "Жанр";
            // 
            // textBoxAuthor
            // 
            textBoxAuthor.Location = new Point(75, 55);
            textBoxAuthor.Name = "textBoxAuthor";
            textBoxAuthor.Size = new Size(200, 23);
            textBoxAuthor.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 58);
            label2.Name = "label2";
            label2.Size = new Size(40, 15);
            label2.TabIndex = 2;
            label2.Text = "Автор";
            // 
            // textBoxTitle
            // 
            textBoxTitle.Location = new Point(75, 22);
            textBoxTitle.Name = "textBoxTitle";
            textBoxTitle.Size = new Size(200, 23);
            textBoxTitle.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 25);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 0;
            label1.Text = "Название";
            // 
            // groupBoxFunctions
            // 
            groupBoxFunctions.Controls.Add(listBoxYearResults);
            groupBoxFunctions.Controls.Add(buttonFindByYear);
            groupBoxFunctions.Controls.Add(textBoxYearFilter);
            groupBoxFunctions.Controls.Add(label5);
            groupBoxFunctions.Controls.Add(listBoxGenres);
            groupBoxFunctions.Controls.Add(buttonGroupByGenre);
            groupBoxFunctions.Location = new Point(10, 405);
            groupBoxFunctions.Name = "groupBoxFunctions";
            groupBoxFunctions.Size = new Size(774, 156);
            groupBoxFunctions.TabIndex = 2;
            groupBoxFunctions.TabStop = false;
            groupBoxFunctions.Text = "Бизнес-функции";
            // 
            // listBoxYearResults
            // 
            listBoxYearResults.FormattingEnabled = true;
            listBoxYearResults.ItemHeight = 15;
            listBoxYearResults.Location = new Point(400, 55);
            listBoxYearResults.Name = "listBoxYearResults";
            listBoxYearResults.Size = new Size(350, 79);
            listBoxYearResults.TabIndex = 5;
            // 
            // buttonFindByYear
            // 
            buttonFindByYear.Location = new Point(519, 22);
            buttonFindByYear.Name = "buttonFindByYear";
            buttonFindByYear.Size = new Size(146, 23);
            buttonFindByYear.TabIndex = 4;
            buttonFindByYear.Text = "Найти книги новее";
            buttonFindByYear.UseVisualStyleBackColor = true;
            buttonFindByYear.Click += buttonFindByYear_Click;
            // 
            // textBoxYearFilter
            // 
            textBoxYearFilter.Location = new Point(433, 21);
            textBoxYearFilter.Name = "textBoxYearFilter";
            textBoxYearFilter.Size = new Size(80, 23);
            textBoxYearFilter.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(400, 25);
            label5.Name = "label5";
            label5.Size = new Size(29, 15);
            label5.TabIndex = 2;
            label5.Text = "Год:";
            // 
            // listBoxGenres
            // 
            listBoxGenres.FormattingEnabled = true;
            listBoxGenres.ItemHeight = 15;
            listBoxGenres.Location = new Point(10, 55);
            listBoxGenres.Name = "listBoxGenres";
            listBoxGenres.Size = new Size(350, 79);
            listBoxGenres.TabIndex = 1;
            // 
            // buttonGroupByGenre
            // 
            buttonGroupByGenre.Location = new Point(10, 25);
            buttonGroupByGenre.Name = "buttonGroupByGenre";
            buttonGroupByGenre.Size = new Size(173, 23);
            buttonGroupByGenre.TabIndex = 0;
            buttonGroupByGenre.Text = "Группировать по жанрам";
            buttonGroupByGenre.UseVisualStyleBackColor = true;
            buttonGroupByGenre.Click += buttonGroupByGenre_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(groupBoxFunctions);
            Controls.Add(groupBoxEdit);
            Controls.Add(dataGridViewBooks);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewBooks).EndInit();
            groupBoxEdit.ResumeLayout(false);
            groupBoxEdit.PerformLayout();
            groupBoxFunctions.ResumeLayout(false);
            groupBoxFunctions.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridViewBooks;
        private GroupBox groupBoxEdit;
        private GroupBox groupBoxFunctions;
        private TextBox textBoxTitle;
        private Label label1;
        private Button buttonAdd;
        internal TextBox textBoxYear;
        private Label label4;
        private TextBox textBoxGenre;
        private Label label3;
        private TextBox textBoxAuthor;
        private Label label2;
        private Button buttonDelete;
        private Button buttonUpdate;
        private ListBox listBoxGenres;
        private Button buttonGroupByGenre;
        private ListBox listBoxYearResults;
        private Button buttonFindByYear;
        private TextBox textBoxYearFilter;
        private Label label5;
    }
}
