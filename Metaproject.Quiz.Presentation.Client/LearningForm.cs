using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autofac.Core.Lifetime;
using Metaproject.Quiz.Application.Core;
using Metaproject.Quiz.Domain.Entities;
using Metaproject.Quiz.Inf.LearningService;

namespace Metaproject.Quiz.Presentation.Client
{
    public partial class LearningForm : Form
    {
        private enum StatesTypeEnum
        {
            Answer,
            Validation
        }

        private StatesTypeEnum _currentState = StatesTypeEnum.Answer;
        private QuestionTable _currentQuestion;
        private LearningStatus _status;

        ILearningService _learningService = new LearningService();

        public LearningForm()
        {
            InitializeComponent();
        }

        public void AttachQuestions(List<QuestionTable> questions)
        {

            var result = _learningService.SetupAndGetFirstQuestion(questions);

            _currentQuestion = result.NextQuestion;
            _status = result.Status;

            SetDialog();
        }

        void SetDialog()
        {
            textBox1.Text = _currentQuestion.Question;
            button2.Enabled = _currentState == StatesTypeEnum.Answer;

            button1.Enabled = _currentState == StatesTypeEnum.Validation;
            button3.Enabled = _currentState == StatesTypeEnum.Validation;

            textBox2.Text = _currentState == StatesTypeEnum.Answer ? "" : _currentQuestion.Answer;

            textBox5.Text = _status.New.ToString();
            textBox3.Text = _status.Learning.ToString();
            textBox4.Text = _status.Memorized.ToString();
            
        }

        // memorized clicked
        private void Button1_Click(object sender, EventArgs e)
        {
            var result = _learningService.ProcessResultAndGetNextQuestion(_currentQuestion, QuestionResult.Memorized);

            _currentQuestion = result.NextQuestion;
            _status = result.Status;
            _currentState = StatesTypeEnum.Answer;

            SetDialog();
        }

        // again clicked
        private void Button3_Click(object sender, EventArgs e)
        {
            var result = _learningService.ProcessResultAndGetNextQuestion(_currentQuestion, QuestionResult.Again);

            _currentQuestion = result.NextQuestion;
            _status = result.Status;
            _currentState = StatesTypeEnum.Answer;

            SetDialog();
        }

        // show
        private void Button2_Click(object sender, EventArgs e)
        {
            _currentState = StatesTypeEnum.Validation;

            SetDialog();
        }

        

    }
}
