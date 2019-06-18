using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Metaproject.Quiz.Application.Core;
using Metaproject.Quiz.Application.Core.Infrastucture.Quiz;

namespace Metaproject.Quiz.Presentation.Client
{
    public partial class ClientForm : Form
    {
        private readonly IWordFilesRepository _wordFilesRepository;
        private readonly Lazy<IQuestionGenerator> _questionGenerator;

        public ClientForm(IWordFilesRepository wordFilesRepository, Lazy<IQuestionGenerator> questionGenerator)
        {
            _wordFilesRepository = wordFilesRepository;
            this._questionGenerator = questionGenerator;
                        
            InitializeComponent();

            checkedListBox1.Click += CheckedListBox1_SelectedValueChanged;
            dataGridView1.DataSource = TableQuestionModel;
        }

        List<Domain.Entities.QuestionTable> _allTables = new List<Domain.Entities.QuestionTable>();
        List<string> _tags = new List<string>();

        private void button1_Click(object sender, EventArgs e)
        {
            var path = textBox1.Text;

            var files = _wordFilesRepository.GetAllDocuments(path);

            _allTables = files.SelectMany(f => f.Tables).ToList();
            _tags = _allTables.SelectMany(t => t.Tags).Distinct().OrderBy(s => s).ToList();
            

            for (int i = 0; i < _tags.Count; i++)
                checkedListBox1.Items.Insert(i, _tags[i]);


            dataGridView1.Refresh();
                       
        }

        List<Domain.Entities.QuestionTable> TableQuestionModel
        {
            get
            {
                return _allTables.Where(t => _tags.Intersect(t.Tags).ToList().Count > 0).ToList();
            }
        }



        
        

        private void Button2_Click(object sender, EventArgs e)
        {
            var tables = (List<Domain.Entities.QuestionTable>)dataGridView1.DataSource;
            var content = _questionGenerator.Value.GenerateQuizQuestions(tables);

            var outputForm = new OutputForm();
            outputForm.AppendOutput(content);
            outputForm.ShowDialog(this);
        }

        private void CheckedListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            var tags = new List<string>();
            foreach (var item in checkedListBox1.SelectedItems)
                tags.Add(item.ToString());

            _tags = tags.ToList();

            dataGridView1.DataSource = TableQuestionModel;

        }
    }
}
