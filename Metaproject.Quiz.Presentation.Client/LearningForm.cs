using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            Validation,
            NothingElseToLearn
        }

        private StatesTypeEnum _currentState = StatesTypeEnum.Answer;
        private QuestionTable _currentQuestion;
        private LearningStatus _status;
        private List<QuestionTable> _questions;

        private TextBox _answerTextBox = new TextBox();
        private PictureBox _answerPictureBox = new PictureBox();

        ILearningService _learningService = new LearningService();

        public LearningForm(List<QuestionTable> questions)
        {
            _questions = questions;

            InitializeComponent();
            InitializeDynamicComponents();

            StartLearning();
        }

        void InitializeDynamicComponents()
        {
            _answerTextBox.Margin = new Padding(10,10,10,10);
            _answerTextBox.Dock = DockStyle.Fill;
            _answerTextBox.Multiline = true;
            _answerTextBox.BorderStyle = BorderStyle.None;
            _answerTextBox.Font = (Font) textBox1.Font.Clone();
            _answerTextBox.BackColor = textBox1.BackColor;

            _answerPictureBox.Margin = new Padding(10, 10, 10, 10);
            _answerPictureBox.Dock = DockStyle.Fill;
            _answerPictureBox.BorderStyle = BorderStyle.None;
            _answerPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }


        void StartLearning()
        {
            var result = _learningService.SetupAndGetFirstQuestion(_questions);

            _currentState = StatesTypeEnum.Answer;
            _currentQuestion = result.NextQuestion;
            _status = result.Status;

            SetDialog();
        }

        void SetDialog()
        {
            textBox1.Text = _currentQuestion?.Question;
            button2.Enabled = _currentState == StatesTypeEnum.Answer;

            button1.Enabled = _currentState == StatesTypeEnum.Validation;
            button3.Enabled = _currentState == StatesTypeEnum.Validation;

            _answerTextBox.Text = _currentState == StatesTypeEnum.Answer ? "" : _currentQuestion?.Answer;

            textBox5.Text = _status.New.ToString();
            textBox3.Text = _status.Learning.ToString();
            textBox4.Text = _status.Memorized.ToString();

            label4.Visible = _currentState == StatesTypeEnum.NothingElseToLearn;

            tableLayoutPanel1.Controls.Remove(_answerPictureBox);
            tableLayoutPanel1.Controls.Remove(_answerTextBox);
            _answerPictureBox.Visible = _currentState != StatesTypeEnum.Answer;
            _answerTextBox.Visible = _currentState != StatesTypeEnum.Answer;

            if (null != _currentQuestion)
            {
                if (_currentQuestion?.AnswerType == AnswerTypeEnum.Image)
                {
                    _answerPictureBox.Image = ByteToImage(_currentQuestion.AnswerAsImage);
                    tableLayoutPanel1.Controls.Add(_answerPictureBox, 1, 1);
                }
                else
                {
                    tableLayoutPanel1.Controls.Add(_answerTextBox, 1, 1);
                }
            }
        }

        // memorized clicked
        private void Button1_Click(object sender, EventArgs e)
        {
            var result = _learningService.ProcessResultAndGetNextQuestion(_currentQuestion, QuestionResult.Memorized);


            _currentQuestion = result.NextQuestion;
            _status = result.Status;


            if (result.Status.IsAnythingToLearn)
            {
                _currentState = StatesTypeEnum.Answer;
            }
            else
            {
                _currentState = StatesTypeEnum.NothingElseToLearn;
            }
            
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

        private void Button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            StartLearning();
        }

        public static Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }
    }
}
