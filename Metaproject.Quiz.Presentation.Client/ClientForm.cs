using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Metaproject.Quiz.Application.Core;
using Metaproject.Quiz.Application.Core.Infrastucture.Quiz;
using Metaproject.Quiz.Domain.Entities;
using Metaproject.Quiz.Presentation.Client.ViewModels;

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
          
            dataGridView1.DataSource = TableQuestionModel;
        }

        List<QuestionTable> _allTables = new List<QuestionTable>();
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
        
        private void Button2_Click(object sender, EventArgs e)
        {
            var tablesModel = (List<QuestionViewModel>)dataGridView1.DataSource;

            var tables = tablesModel.Select(t => t.Table).ToList();
            
            var content = _questionGenerator.Value.GenerateQuizQuestions(tables);

            var outputForm = new OutputForm();
            outputForm.AppendOutput(content);
            outputForm.ShowDialog(this);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            var tablesModel = (List<QuestionViewModel>)dataGridView1.DataSource;
            var questions = tablesModel.Select(t => t.Table).ToList();

            var learnForm = new LearningForm(questions);
            learnForm.ShowDialog(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var tags = new List<string>();
            foreach (var item in checkedListBox1.CheckedItems)
                tags.Add(item.ToString());

            _tags = tags.ToList();

            dataGridView1.DataSource = TableQuestionModel;
            dataGridView1.Refresh();
        }

        private List<QuestionViewModel> TableQuestionModel => _allTables
            .Where(t => t.Tags.Intersect(_tags).Count() == _tags.Count).Select(q => new QuestionViewModel(q)).ToList();


        
    }
}
