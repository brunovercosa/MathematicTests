using System.ComponentModel;
using System.Reflection;

namespace MathematicTests
{
    public partial class frmChallenge : Form
    {
        Random random = new Random();
        int numberOne, numberTwo;
        int timeLeft;
        int response;
        int points;
        int timeGame = 60;
        public frmChallenge()
        {
            InitializeComponent();
        }

        private void btnStartQuiz_Click(object sender, EventArgs e)
        {
            StartQuiz();
        }

        private void StartQuiz()
        {
            btnEndQuiz.Visible = true;
            btnStartQuiz.Enabled = false;

            numberOne = random.Next(100);
            numberTwo = random.Next(100);

            lblFirstNumberSum.Text = numberOne.ToString();
            lblSecondNumberSum.Text = numberTwo.ToString();

            numberOne = random.Next(100);
            numberTwo = random.Next(100);

            lblFirstNumberSub.Text = numberOne.ToString();
            lblSecondNumberSub.Text = numberTwo.ToString();

            numberOne = random.Next(100);
            numberTwo = random.Next(100);

            lblFirstNumberMult.Text = numberOne.ToString();
            lblSecondNumberMult.Text = numberTwo.ToString();

            numberOne = random.Next(100);
            numberTwo = random.Next(1, 100);

            lblFirstNumberDiv.Text = numberOne.ToString();
            lblSecondNumberDiv.Text = numberTwo.ToString();

            numericUpDownSum.Value = 0;
            numericUpDownSub.Value = 0;
            numericUpDownDiv.Value = 0;
            numericUpDownMult.Value = 0;


            timeLeft = timeGame;
            timer1.Enabled = true;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            txtLeftTime.Text = timeLeft + " segundos";
            CheckAndFinalizeTest(forceFinalize: false);
        }

        private void CheckAndFinalizeTest(bool forceFinalize)
        {
            int pointAnswers = CheckTheAnswer();

            if (pointAnswers == 4 || timeLeft == 0 || forceFinalize)
            {
                timer1.Stop();
                timer1.Enabled = false;
                int time = timeGame - timeLeft;

                string message = "";
                if (timeLeft == 0 || forceFinalize)
                {
                    message = "Você acertou " + pointAnswers + " questões em: " + time;
                }
                else
                {
                    message = "Parabéns você acertou as 4 questões em: " + time;
                }

                MessageBox.Show(message);

                var resultMessage = MessageBox.Show("Deseja iniciar um novo jogo?", "Iniciar novo", 
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if(resultMessage == DialogResult.Yes)
                {
                    StartQuiz();
                }
                else
                {
                    Close();
                }
            }
        }

        private void btnEndQuiz_Click(object sender, EventArgs e)
        {
            CheckAndFinalizeTest(forceFinalize: true);
        }

        public int CheckTheAnswer()
        {
            points = 0;

            numberOne = Convert.ToInt32(lblFirstNumberSum.Text);
            numberTwo = Convert.ToInt32(lblSecondNumberSum.Text);
            response = Convert.ToInt32(numericUpDownSum.Value);

            if((numberOne + numberTwo) == response)
            {
                points++;
            }

            numberOne = Convert.ToInt32(lblFirstNumberSub.Text);
            numberTwo = Convert.ToInt32(lblSecondNumberSub.Text);
            response = Convert.ToInt32(numericUpDownSub.Value);

            if ((numberOne - numberTwo) == response)
            {
                points++;
            }

            numberOne = Convert.ToInt32(lblFirstNumberMult.Text);
            numberTwo = Convert.ToInt32(lblSecondNumberMult.Text);
            response = Convert.ToInt32(numericUpDownMult.Value);

            if ((numberOne * numberTwo) == response)
            {
                points++;
            }

            decimal numberOneDec = Convert.ToDecimal(lblFirstNumberDiv.Text);
            decimal numberTwoDec = Convert.ToDecimal(lblSecondNumberDiv.Text);

            if (Math.Truncate(numberOneDec / numberTwoDec) == numericUpDownDiv.Value)
            {
                points++;
            }

            return points;
        }
    }
}