using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Data;
using System.IO;

namespace oltp2olap.heuristics
{
    class Classification
    {
        private int[,] graphTables;
        private int[] weight;
        private int[] doubleWeighted;
        private List<int> transactionEntities = new List<int>();
        private List<int> componentEntities = new List<int>();
        private List<int> classificationEntities = new List<int>();
        private List<int> maximalEntities = new List<int>();
        private List<int> minimalEntities = new List<int>();
        private List<LinkedList<int>> maximalHierarchies = new List<LinkedList<int>>();
        private List<LinkedList<string>> maximalStringHierarchies = new List<LinkedList<string>>();
        private int numTables = 0;
        private Hashtable hashTables;
        private List<String> vectTables;
        private List<String> vectTablesClone;
        private List<LinkedList<int>> graphVector = null;

        // doublyLinked faz par com doublyLinkedStrings:
        // para cada doublyLinked uma LinkedStrings
        public List<int> doublyLinked = new List<int>();

        public List<LinkedList<String>> doublyLinkedStrings = new List<LinkedList<String>>();

        public List<LinkedList<String>> inputFromSQL;

        public Classification(DataSet ds, List<string> visible)
        {
            numTables = visible.Count;

            graphTables = new int[numTables, numTables];
            weight = new int[numTables];
            doubleWeighted = new int[numTables];
            hashTables = new Hashtable(numTables);
            vectTables = new List<String>(numTables);
            inputFromSQL = new List<LinkedList<String>>();

            foreach (DataRelation rel in ds.Relations)
            {
                if (visible.Contains(rel.ChildTable.TableName) &&
                    visible.Contains(rel.ParentTable.TableName))
                {
                    LinkedList<String> lines = new LinkedList<String>();
                    lines.AddLast(rel.ChildTable.TableName);
                    lines.AddLast(rel.ChildColumns[0].ColumnName);
                    lines.AddLast(rel.ParentTable.TableName);
                    lines.AddLast(rel.ParentColumns[0].ColumnName);
                    lines.AddLast(rel.RelationName);
                    inputFromSQL.Add(lines);
                }
            }

            IEnumerator inputIt = inputFromSQL.GetEnumerator();
            int tabs = 0;

            graphVector = new List<LinkedList<int>>();
            for (int i = 0; i < numTables; i++)
                graphVector.Insert(i, new LinkedList<int>());

            while (inputIt.MoveNext())
            {
                List<String> listTab = new List<string>((LinkedList<string>)inputIt.Current);
                for (int j = 0; j < 2; j++)
                {
                    String table = listTab[j * 2];
                    object found = hashTables[table];
                    if (found == null)
                    {
                        vectTables.Insert(tabs, table);
                        hashTables.Add(table, tabs);
                        tabs++;
                    }
                }
                int x = vectTables.IndexOf(listTab[0]);
                int y = vectTables.IndexOf(listTab[2]);
                graphTables[x, y] += 1;
                if (graphTables[x, y] > 1 && !doublyLinked.Contains(y))
                    doublyLinked.Add(y);
                LinkedList<int> tmp = (LinkedList<int>)graphVector[y];
                if (!tmp.Contains(x))
                    tmp.AddFirst(x);
                graphVector[y] = tmp;
                weight[x] += 1;
                doubleWeighted[y] += 1;
            }
        }

        private void resetAll()
        {
            transactionEntities.Clear();
            componentEntities.Clear();
            classificationEntities.Clear();
        }

        private void firstAlgoritmVersion()
        {
            resetAll();
            int max = 0;
            int ind = 0;
            int prn = 0;
            int i;
            // procura do máximo de ligações = find de uma tabela de transacção ("my
            // way"/ bad way)
            for (int j = 0; j < numTables; j++)
            {
                prn = (doubleWeighted[j] + weight[j]);
                if (prn > max)
                {
                    max = prn;
                    ind = j;
                }
            }
            // Algoritmo de sugestão/ajuda na selecção das tabelas
            transactionEntities.Add(ind);
            // ind é pos da tabela com mais ligações:
            // todas as referencias para ela sao de transactions tabelas tb
            for (i = 0; i < numTables; i++)
            {
                if (graphTables[i, ind] > 0)
                    transactionEntities.Add(i);
            }
            i = 0;
            IEnumerator transIterator = transactionEntities.GetEnumerator();
            while (transIterator.MoveNext())
            {
                int j = (int)transIterator.Current;
                for (i = 0; i < numTables; i++)
                {
                    if (graphTables[j, i] > 0
                            && !transactionEntities.Contains(i))
                        componentEntities.Add(i);
                }
            }

            // tabelas de entidades sao tabelas de "nivel 3" (tabelas ligadas as de
            // "nivel 2")
            // mais as q sobram...
            i = 0;
            IEnumerator compIterator = componentEntities.GetEnumerator();
            compIterator = componentEntities.GetEnumerator();
            while (compIterator.MoveNext())
            {
                int j = (int)compIterator.Current;
                for (i = 0; i < numTables; i++)
                {
                    int tmp = i;
                    if (graphTables[j, i] > 0
                            && !classificationEntities.Contains(tmp)
                            && !transactionEntities.Contains(tmp))
                        classificationEntities.Add(tmp);
                }
            }

            vectTablesClone = new List<string>(vectTables);
            transIterator = transactionEntities.GetEnumerator();
            while (transIterator.MoveNext())
            {
                if ((int)transIterator.Current < vectTables.Count)
                {
                    if (vectTablesClone.Contains(vectTables[(int)transIterator.Current]))
                        vectTablesClone.Remove(vectTables[(int)transIterator.Current]);
                }
            }

            compIterator = componentEntities.GetEnumerator();
            while (compIterator.MoveNext())
            {
                if ((int)compIterator.Current < vectTables.Count)
                {
                    if (vectTablesClone.Contains(vectTables[(int)compIterator.Current]))
                        vectTablesClone.Remove(vectTables[(int)compIterator.Current]);
                }
            }

            // Faz add do resto
            IEnumerator cloneIterator = vectTablesClone.GetEnumerator();
            while (cloneIterator.MoveNext())
            {
                int tmp = vectTables.IndexOf((string)cloneIterator.Current);
                if (!classificationEntities.Contains(tmp))
                    classificationEntities.Add((int)tmp);
            }
        }

        private void entitiesClassification()
        {
            resetAll();
            int j;
            // primeiras tabelas de transactions:
            // tabelas sem referencia
            // tb sao entidades minimas

            for (j = 0; j < numTables; j++)
            {
                if (doubleWeighted[j] == 0)
                {
                    transactionEntities.Add(j);
                }
            }

            // procurar ligações em comum dessas mesmas tabelas: tb sao tab de
            // transactions
            IEnumerator transIterator;
            bool isTrans;
            List<int> tmpVect = new List<int>();
            for (j = 0; j < numTables; j++)
            {
                transIterator = transactionEntities.GetEnumerator();
                isTrans = true;
                while (transIterator.MoveNext())
                {
                    int trans = (int)transIterator.Current;
                    if (graphTables[trans, j] != 1)
                        isTrans = false;
                }
                if (isTrans)
                    tmpVect.Add(j);
            }

            IEnumerator tmpIterator = tmpVect.GetEnumerator();
            while (tmpIterator.MoveNext())
                transactionEntities.Add((int)tmpIterator.Current);
            /*
             * 
             */
            // todas as tabelas de "nivel 2" sao tabelas componente
            // essas tabelas sao as q estao ligadas as de transactions
            int i = 0;
            j = 0;
            transIterator = transactionEntities.GetEnumerator();
            transIterator = transactionEntities.GetEnumerator();
            while (transIterator.MoveNext())
            {
                j = (int)transIterator.Current;
                for (i = 0; i < numTables; i++)
                {
                    if (graphTables[j, i] > 0
                            && !transactionEntities.Contains((i)))
                        componentEntities.Add((i));
                }
            }

            // tabelas de entidades sao tabelas de "nivel 3" (tabelas ligadas as de
            // "nivel 2")
            // mais as q sobram...
            i = 0;
            j = 0;
            IEnumerator compIterator = componentEntities.GetEnumerator();
            compIterator = componentEntities.GetEnumerator();
            while (compIterator.MoveNext())
            {
                j = ((int)compIterator.Current);
                for (i = 0; i < numTables; i++)
                {
                    int tmp = (i);
                    if (graphTables[j, i] > 0
                            && !classificationEntities.Contains(tmp)
                            && !transactionEntities.Contains(tmp))
                        classificationEntities.Add(tmp);
                }
            }

            vectTablesClone = new List<string>(vectTables);
            transIterator = transactionEntities.GetEnumerator();
            while (transIterator.MoveNext())
            {
                if ((int)transIterator.Current < vectTables.Count)
                {
                    if (vectTablesClone.Contains(vectTables[(int)transIterator.Current]))
                        vectTablesClone.Remove(vectTables[(int)transIterator.Current]);
                }
            }

            compIterator = componentEntities.GetEnumerator();
            while (compIterator.MoveNext())
            {
                if ((int)compIterator.Current < vectTables.Count)
                {
                    if (vectTablesClone.Contains(vectTables[(int)compIterator.Current]))
                        vectTablesClone.Remove(vectTables[(int)compIterator.Current]);
                }
            }


            // Faz add do resto
            IEnumerator cloneIterator = vectTablesClone.GetEnumerator();
            while (cloneIterator.MoveNext())
            {
                int tmp = vectTables.IndexOf((string)cloneIterator.Current);
                if (!classificationEntities.Contains(tmp))
                    classificationEntities.Add((int)tmp);
            }
        }

        private int nextEntity(int entity, int pos)
        {
            for (int i = pos; i < numTables; i++)
                if (graphTables[i, entity] != 0)
                    return i;
            return -1;
        }

        private void populateDoublyLinkedString()
        {
            IEnumerator it = doublyLinked.GetEnumerator();
            while (it.MoveNext())
            {
                int value = (int)it.Current;
                String toCompare = vectTables[value];
                IEnumerator inputIt = inputFromSQL.GetEnumerator();
                List<String> res = new List<String>();
                while (inputIt.MoveNext())
                {
                    List<String> line = new List<string>((LinkedList<string>) inputIt.Current);
                    if (line[2].Equals(toCompare))
                    {
                        res.Add(line[1]);
                    }
                }
                LinkedList<string> newRes = new LinkedList<string>(res);
                doublyLinkedStrings.Add(newRes);
            }
        }

        private LinkedList<LinkedList<int>> hierarchyProducer(int entity)
        {
            LinkedList<LinkedList<int>> res = new LinkedList<LinkedList<int>>();
            LinkedList<int> currHierarchy = new LinkedList<int>();
            currHierarchy.AddFirst(entity);// entity received in head
            res.AddFirst(currHierarchy);// firstOne
            bool done = false;
            int seen = 0;
            while (!done)
            {
                LinkedList<LinkedList<int>> tmpRes = new LinkedList<LinkedList<int>>();
                IEnumerator resIt = res.GetEnumerator();
                while (resIt.MoveNext())
                {
                    LinkedList<int> fromRes = (LinkedList<int>)resIt.Current;
                    int lastEntity = (int)fromRes.First.Value;
                    if (!minimalEntities.Contains(lastEntity))
                    {
                        LinkedList<int> fromGraph = graphVector[(int)lastEntity];
                        int listSize = fromGraph.Count;
                        if (listSize > 1)
                        {
                            IEnumerator fgIt = fromGraph.GetEnumerator();
                            while (fgIt.MoveNext())
                            {
                                LinkedList<int> tmp = new LinkedList<int>((LinkedList<int>)fromRes);
                                int lastVal = (int)fgIt.Current;
                                tmp.AddFirst(lastVal);
                                tmpRes.AddFirst(tmp);
                                if (minimalEntities.Contains(lastVal))
                                    seen++;
                            }
                        }
                        else
                        {
                            int toAdd = (int)fromGraph.First.Value;
                            fromRes.AddFirst(toAdd);
                            tmpRes.AddFirst(fromRes);
                            if (minimalEntities.Contains(toAdd))
                                seen++;
                        }
                        if (seen == tmpRes.Count)
                            done = true;
                    }
                }
                if (tmpRes.Count == 0)
                    done = true;

                res = new LinkedList<LinkedList<int>>((LinkedList<LinkedList<int>>)tmpRes);
            }
            return res;
        }

        private void hierarchiesValidator(List<LinkedList<int>> vector)
        {
            IEnumerator maxHIt = maximalHierarchies.GetEnumerator();
            while (maxHIt.MoveNext())
            {
                LinkedList<int> aList = (LinkedList<int>)maxHIt.Current;
                IEnumerator listIt = aList.GetEnumerator();
                LinkedList<String> res = new LinkedList<String>();
                while (listIt.MoveNext())
                {
                    int value = (int)listIt.Current;
                    String toAdd;
                    toAdd = vectTables[value];
                    res.AddFirst(toAdd);
                }
                maximalStringHierarchies.Add(res);
            }
            // fazer iteration da lista dos duplos e fazer sub progressivo
            // search por string!
            IEnumerator doubleLink = doublyLinked.GetEnumerator();
            while (doubleLink.MoveNext())
            {
                int toSubstitute;
                String toSubString;
                toSubstitute = (int)doubleLink.Current;
                toSubString = vectTables[toSubstitute];// ja tenho a string
                // a
                // procurar
                // fazer uma lista dos q vou fazer remover, duplicate e dpx add
                // again from
                // maximalStringHierarchies
                List<LinkedList<String>> toProcess = new List<LinkedList<String>>();
                List<LinkedList<String>> maxSHClone;
                maxSHClone = new List<LinkedList<String>>(maximalStringHierarchies);
                IEnumerator maxStrHIt = maximalStringHierarchies.GetEnumerator();
                while (maxStrHIt.MoveNext())
                {
                    LinkedList<String> elem = (LinkedList<String>)maxStrHIt.Current;
                    if (elem.Contains(toSubString))
                    {
                        toProcess = new List<LinkedList<String>>();
                        toProcess.Add(elem);
                        maxSHClone.Remove(elem);
                        // tenho a list, a string e a pos da ligação ;)
                        toProcess = multiplicateLast(toProcess, toSubString, toSubstitute);
                        IEnumerator procIt = toProcess.GetEnumerator();
                        while (procIt.MoveNext())
                        {
                            LinkedList<String> adder = new LinkedList<string>((LinkedList<string>)procIt.Current);
                            maxSHClone.Add(adder);
                        }
                    }
                }
                maximalStringHierarchies = new List<LinkedList<string>>((List<LinkedList<String>>)(maxSHClone));
            }
        }

        //  toProcess vem sempre so com 1 elem mas vai com mais (output) 
        private List<LinkedList<String>> multiplicateLast(List<LinkedList<String>> toProcess,
                String toSubString, int toSubstitute)
        {
            int pos = doublyLinked.IndexOf(toSubstitute);
            LinkedList<String> subsitutes = doublyLinkedStrings[pos];
            List<LinkedList<String>> res = new List<LinkedList<String>>();
            List<String> toManage = new List<string>(toProcess[0]);

            //while(hasnext) {

            int indexOf = toManage.IndexOf(toSubString);
            IEnumerator sIt = subsitutes.GetEnumerator();
            while (sIt.MoveNext() && indexOf != -1)
            {
                toManage = new List<string>(toProcess[0]);
                String tmp = toSubString + "(" + (String)sIt.Current + ")";
                toManage.RemoveAt(indexOf);
                toManage.Insert(indexOf, tmp);
                res.Add(new LinkedList<string>(toManage));
            }
            //    }
            return (List<LinkedList<String>>)res;
        }

        public void CalculateHierarquies()
        {
            for( int j = 0; j < numTables; j++ )
            {
                if( doubleWeighted[ j ] == 0 )
                {
                    minimalEntities.Add( j );
                }
            }
        
            for( int j = 0; j < numTables; j++ )
            {
                if( weight[ j ] == 0 )
                {
                    maximalEntities.Add ( j );
                }

            }

            IEnumerator maxEntIt = maximalEntities.GetEnumerator();
            while (maxEntIt.MoveNext())
            {
                int param = (int)maxEntIt.Current;
                LinkedList<LinkedList<int>> produced = hierarchyProducer(param);
                IEnumerator prodIt = produced.GetEnumerator();
                while (prodIt.MoveNext())
                    maximalHierarchies.Add((LinkedList<int>)prodIt.Current);
            }
            populateDoublyLinkedString();
            hierarchiesValidator(maximalHierarchies);
        }


        public Dictionary<string, EntityTypes> ClassificateEntities(ClassificationTypes algorithm)
        {
            if (algorithm.Equals(ClassificationTypes.AlgorithmNumber1))
                firstAlgoritmVersion();
            else if (algorithm.Equals(ClassificationTypes.AlgorithmNumber2))
                entitiesClassification();

            Dictionary<string, EntityTypes> entities = new Dictionary<string,EntityTypes>();
            IEnumerator iter = transactionEntities.GetEnumerator();
            while (iter.MoveNext())
            {
                int tmp = (int)iter.Current;
                if (tmp < vectTables.Count)
                    entities[vectTables[tmp]] = EntityTypes.TransactionEntity;
            }

            iter = componentEntities.GetEnumerator();
            while (iter.MoveNext())
            {
                int tmp = (int)iter.Current;
                if (tmp < vectTables.Count)
                    entities[vectTables[tmp]] = EntityTypes.ComponentEntity;
            }

            iter = classificationEntities.GetEnumerator();
            while (iter.MoveNext())
            {
                int tmp = (int)iter.Current;
                if (tmp < vectTables.Count)
                    entities[vectTables[tmp]] = EntityTypes.ClassificationEntity;
            }

            return entities;
        }

        public List<string> MinimalEntities
        {
            get {
                if (vectTables.Count == 0)
                    return new List<string>();

                List<string> tables = new List<string>();
                foreach (int tableIdx in minimalEntities)
                    tables.Add(vectTables[tableIdx]);

                return tables;
            }
        }

        public List<LinkedList<string>> MaximalStringHierarchies
        {
            get { return maximalStringHierarchies; }
        }

    }
}

