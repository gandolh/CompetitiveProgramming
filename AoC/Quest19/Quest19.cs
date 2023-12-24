
using BenchmarkDotNet.Code;
using Microsoft.Diagnostics.Runtime;
using Microsoft.Diagnostics.Runtime.Utilities;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;


namespace AoC.Quest19
{
    internal class Quest19 : BaseQuest
    {
        Dictionary<string, Func<int,int,int,int, bool>> Automatas { get; set; } = [];

        public override Task Solve()
        {
            string inPath = GetPathTo("quest19_0.in");
            string outPath = GetPathTo("questResult.out");
            File.WriteAllText(outPath, "");
            string[] lines = File.ReadAllLines(inPath);
            int stopIndex = ReadAutomatas(lines);
            
            // read xmas values
            for(int i = stopIndex; i < lines.Length; i++)
            {
                string line = lines[i].Replace("{","").Replace("}","");
                int[] values = line.Split(",").Select(el => Int32.Parse(el.Split("=")[1])).ToArray();
            }

            return Task.CompletedTask;
        }

        private int ReadAutomatas(string[] lines)
        {
            int i;
            for( i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "") break;
                string[] splitArray = lines[i].Split('{');
                string label = splitArray[0];
                string allConditions = splitArray[1];
                allConditions = allConditions.Replace("}","");
                BuildFunction(label, allConditions);

            }
            return i + 1;
        }

        private void BuildFunction(string label, string allConditions)
        {
            
            ParameterExpression paramX = Expression.Parameter(typeof(int), "x");
            ParameterExpression paramM = Expression.Parameter(typeof(int), "m");
            ParameterExpression paramA = Expression.Parameter(typeof(int), "a");
            ParameterExpression paramS = Expression.Parameter(typeof(int), "s");
            List<Expression> conditionExpressions = new List<Expression>();


            string[] conditions = allConditions.Split(",");
            for (int j = 0; j < conditions.Length; j++)
            {
                string[] splitArrayCondition = conditions[j].Split(":");
                if(j == conditions.Length - 1)
                {
                    string defaultCase = splitArrayCondition[0];
                    conditionExpressions.Add(Expression.Condition(Expression.Constant(true), 
                        GetActionExpression(defaultCase, paramX, paramM, paramA, paramS), 
                        Expression.Constant(false)));
                    continue;
                }

                string lhs = splitArrayCondition[0];
                string rhs = splitArrayCondition[1];
                (Expression Test, Expression IfTrue, Expression IfElse) = GetExpresions(lhs,rhs, paramX, paramM, paramA, paramS);
                conditionExpressions.Add(Expression.Condition(Test, IfTrue, IfElse));
            }

            Expression finalBody = conditionExpressions[0];
            for (int i = 1; i < conditionExpressions.Count; i++)
                    finalBody = Expression.AndAlso(finalBody, conditionExpressions[i]);

            // Creating the lambda expression that represents the function
            Expression<Func<int, int, int, int, bool>> dynamicFunction = Expression
                .Lambda<Func<int, int, int, int, bool>>(finalBody, paramX,
                paramM, paramA, paramS);

            // Compiling the lambda expression to create the actual function
            Func<int, int, int, int, bool> evalFunction = dynamicFunction.Compile();
            Automatas[label] = evalFunction;
        }

        private Expression GetActionExpression(string rhs,
            ParameterExpression paramX, ParameterExpression paramM, ParameterExpression paramA, ParameterExpression paramS)
        {
            Expression actionExpression;
            if (rhs.Last() == 'A')
                actionExpression = Expression.Constant(true);
            else if (rhs.Last() == 'R')
                actionExpression = Expression.Constant(false);
            else
            {
                dynamic instance;
                if (!Automatas.ContainsKey(rhs))
                {
                    Automatas[rhs] = this.DefaultFunction;
                    instance = this;
                }
                else
                {
                    instance = this.Automatas;
                }

                MethodInfo methodInfo = Automatas[rhs].Method;
                actionExpression = Expression.Call(Expression.Constant(instance), methodInfo, paramX, paramM, paramA, paramS);
            }
            return actionExpression;
        }

        private (Expression Test, Expression IfTrue, Expression IfElse) GetExpresions(string lhs, string rhs, 
            ParameterExpression paramX, ParameterExpression paramM, ParameterExpression paramA, ParameterExpression paramS)
        {
            ParameterExpression param;
            switch (lhs[0])
            {
                case 'x':
                    param = paramX;
                    break;
                case 'm':
                    param = paramM;
                    break;
                case 'a':
                    param = paramA;
                    break;
                case 's':
                    param = paramS;
                    break;
                default:
                    throw new ArgumentException("Invalid parameter specified in condition.");
            }

            BinaryExpression Test;
            int valueToCompare = Int32.Parse(lhs.Substring(2));
            switch (lhs[1])
            {
                case '<':
                    Test = Expression.LessThan(param, Expression.Constant(valueToCompare));
                    break;
                case '>':
                    Test = Expression.GreaterThan(param, Expression.Constant(valueToCompare));
                    break;
                case '=':
                    Test = Expression.Equal(param, Expression.Constant(valueToCompare));
                    break;
                case '!':
                    Test = Expression.NotEqual(param, Expression.Constant(valueToCompare));
                    break;
                default:
                    throw new ArgumentException("Invalid comparison operator specified in condition.");
            }
            Expression actionExpression = GetActionExpression(rhs,paramX, paramM, paramA, paramS);
            return (Test, actionExpression, Expression.Constant(false));
        }
        public bool DefaultFunction(int x, int m, int a, int s)
        {
            Console.WriteLine("x: ", x); return false;
        }
    }

}
