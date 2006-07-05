using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Data;
using System.IO;

namespace oltp2olap.heuristics
{
    public class Classification
    {
        private int[,] graphTables;
        private int[] weight;
        private int[] doubleWeighted;
        private List<int> transactionEntities = new List<int>();
        private List<int> componentEntities = new List<int>();
        private List<int> classificationEntities = new List<int>();
        private List<int> maximalEntities = new List<int>();
        private List<int> minimalEntities = new List<int>();
        private List<List<int>> maximalHierarchies = new List<List<int>>();
        private List<List<string>> maximalStringHierarchies = new List<List<string>>();
        private int numTables = 0;
        private Hashtable hashTables;
        private List<String> vectTables;
        private List<String> vectTablesClone;
        private List<List<int>> graphVector = null;

        // doublyLinked faz par com doublyLinkedStrings:
        // para cada doublyLinked uma LinkedStrings
        public List<int> doublyLinked = new List<int>();

        public List<List<String>> doublyLinkedStrings = new List<List<String>>();

        public List<List<String>> inputFromSQL;

        public Classification(DataSet ds, List<string> visible)
        {
            numTables = visible.Count;

            graphTables = new int[numTables, numTables];
            weight = new int[numTables];
            doubleWeighted = new int[numTables];
            hashTables = new Hashtable(numTables);
            vectTables = new List<String>(numTables);
            inputFromSQL = new List<List<String>>();

            foreach (DataRelation rel in ds.Relations)
            {
                if (visible.Contains(rel.ChildTable.TableName) &&
                    visible.Contains(rel.ParentTable.TableName))
                {
                    List<String> lines = new List<String>();
                    lines.Add(rel.ChildTable.TableName);
                    lines.Add(rel.ChildColumns[0].ColumnName);
                    lines.Add(rel.ParentTable.TableName);
                    lines.Add(rel.ParentColumns[0].ColumnName);
                    lines.Add(rel.RelationName);
                    inputFromSQL.Add(lines);
                }
            }

            IEnumerator inputIt = inputFromSQL.GetEnumerator();
            int tabs = 0;

            graphVector = new List<List<int>>();
            for (int i = 0; i < numTables; i++)
                graphVector.Insert(i, new List<int>());

            while (inputIt.MoveNext())
            {
                List<String> listTab = new List<string>((List<string>)inputIt.Current);
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
                List<int> tmp = (List<int>)graphVector[y];
                if (!tmp.Contains(x))
                    tmp.Insert(0, x);
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
                    List<String> line = new List<string>((List<string>) inputIt.Current);
                    if (line[2].Equals(toCompare) && (!res.Contains(line[1])))
                    {
                        res.Add(line[1]);
                    }
                }
                List<string> newRes = new List<string>(res);
                if(!doublyLinkedStrings.Contains(newRes))
                    doublyLinkedStrings.Add(newRes);
            }
        }

        private List<List<int>> hierarchyProducer(int entity)
        {
            List<List<int>> res = new List<List<int>>();
            List<int> currHierarchy = new List<int>();
            currHierarchy.Insert(0, entity);// entity received in head
            res.Insert(0, currHierarchy);// firstOne
            bool done = false;
            int seen = 0;
            int see = 0;
            while (!done)
            {
                List<List<int>> tmpRes = new List<List<int>>();
                IEnumerator resIt = res.GetEnumerator();
                while (resIt.MoveNext())
                {
                    List<int> fromRes = (List<int>)resIt.Current;
                    int lastEntity = (int)fromRes[0];

                    if (!minimalEntities.Contains(lastEntity))
                    {
                        List<int> fromGraph = graphVector[(int)lastEntity];
                        int listSize = fromGraph.Count;
                        if (listSize > 1)
                        {
                            IEnumerator fgIt = fromGraph.GetEnumerator();
                            while (fgIt.MoveNext())
                            {
                                List<int> tmp = new List<int>((List<int>)fromRes);
                                int lastVal = (int)fgIt.Current;
                                tmp.Insert(0, lastVal);
                                tmpRes.Insert(0, tmp);
                                if (minimalEntities.Contains(lastVal))
                                    seen++;
                            }
                        }
                        else
                        {
                            int toAdd = (int)fromGraph[0];
                            fromRes.Insert(0, toAdd);
                            tmpRes.Insert(0, fromRes);
                            if (minimalEntities.Contains(toAdd))
                                seen++;
                        }
                        see += tmpRes.Count;
                        if (seen >= see)
                            done = true;
                    }
                }
                if (tmpRes.Count == 0)
                {
                    done = true;
                }
                else
                {
                    res = new List<List<int>>((List<List<int>>)tmpRes);
                }
            }
            foreach (int elem in graphVector[entity])
            {
                if (minimalEntities.Contains(elem))
                {
                    List<int> toAdd = new List<int>();
                    toAdd.Add(elem);
                    toAdd.Add(entity);                    
                    if(!res.Contains(toAdd))
                        res.Add(toAdd);
                }
            }
            return res;
        }

        private void hierarchiesValidator(List<List<int>> maxHierarchiesList)
        {
            IEnumerator maxHIt = maximalHierarchies.GetEnumerator();
            while (maxHIt.MoveNext())
            {
                List<int> aList = (List<int>)maxHIt.Current;
                IEnumerator listIt = aList.GetEnumerator();
                List<String> res = new List<String>();
                while (listIt.MoveNext())
                {
                    int value = (int)listIt.Current;
                    String toAdd;
                    if (vectTables.Count > value)
                    {
                        toAdd = vectTables[value];
                        res.Insert(0, toAdd);
                    }
                }
                if(!maximalStringHierarchies.Contains(res))
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
                List<List<String>> toProcess = new List<List<String>>();
                List<List<String>> maxSHClone;
                maxSHClone = new List<List<String>>(maximalStringHierarchies);
                IEnumerator maxStrHIt = maximalStringHierarchies.GetEnumerator();
                while (maxStrHIt.MoveNext())
                {
                    List<String> elem = (List<String>)maxStrHIt.Current;
                    if (elem.Contains(toSubString))
                    {
                        toProcess = new List<List<String>>();
                        toProcess.Add(elem);
                        maxSHClone.Remove(elem);
                        // tenho a list, a string e a pos da ligação ;)
                        toProcess = multiplicateLast(toProcess, toSubString, toSubstitute);
                        IEnumerator procIt = toProcess.GetEnumerator();
                        while (procIt.MoveNext())
                        {
                            List<String> adder = new List<string>((List<string>)procIt.Current);
                            maxSHClone.Add(adder);
                        }
                    }
                }
                maximalStringHierarchies = new List<List<string>>((List<List<String>>)(maxSHClone));
            }
        }

        //  toProcess vem sempre so com 1 elem mas vai com mais (output) 
        private List<List<String>> multiplicateLast(List<List<String>> toProcess,
                String toSubString, int toSubstitute)
        {
            int pos = doublyLinked.IndexOf(toSubstitute);
            List<String> subsitutes = doublyLinkedStrings[pos];
            List<List<String>> res = new List<List<String>>();
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
                res.Add(new List<string>(toManage));
            }
            //    }
            return (List<List<String>>)res;
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
                List<List<int>> produced = hierarchyProducer(param);
                IEnumerator prodIt = produced.GetEnumerator();
                while (prodIt.MoveNext()){
                    List<int> toAdd = (List<int>)prodIt.Current;
                    if (!maximalHierarchies.Contains(toAdd))
                    {
                        maximalHierarchies.Add(toAdd);
                    }
                }
            }
            maximalHierarchies = removesDuplicates(maximalHierarchies);
            populateDoublyLinkedString();
            hierarchiesValidator(maximalHierarchies);
        }

        private List<List<int>> removesDuplicates(List<List<int>> maximalHierarchies)
        {
            List<List<int>> res = new List<List<int>>();
            foreach (List<int> newList in maximalHierarchies)
            {
                bool contains = listOfListsContainsList(res, newList);
                if (!contains)
                {
                    res.Add(newList);
                }                    
            }            
            return res;
        }

        private bool listOfListsContainsList(List<List<int>> listOfLists, List<int> newList)
        {
            bool res = false;
            foreach (List<int> l in listOfLists)
            {
                if (l.Count == newList.Count)
                {
                    int toBcountedEqual = l.Count;
                    int equalCounted = 0;
                    int[] l_array = l.ToArray();
                    int[] nl_array = newList.ToArray();
                    for (int i = 0; i < l.Count;i++ )
                    {
                        if (l_array[i] == nl_array[i])
                            equalCounted++;
                    }
                    if (toBcountedEqual == equalCounted)
                    {
                        res = true;
                    }
                }
            }
            return res;
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

        public List<int> MinimalEntities
        {
            get { return minimalEntities; }
        }

        public List<string> MinimalStringEntities
        {
            get {
                if (vectTables.Count == 0)
                    return new List<string>();

                List<string> tables = new List<string>();
                foreach (int tableIdx in minimalEntities)
                {
                    if (tableIdx < vectTables.Count)
                        tables.Add(vectTables[tableIdx]);
                }

                return tables;
            }
        }

        public List<int> MaximalEntities
        {
            get { return maximalEntities; }
        }

        public List<string> MaximalStringEntities
        {
            get
            {
                if (vectTables.Count == 0)
                    return new List<string>();

                List<string> tables = new List<string>();
                foreach (int tableIdx in maximalEntities)
                {
                    if (tableIdx < vectTables.Count)
                        tables.Add(vectTables[tableIdx]);
                }

                return tables;
            }
        }

        public List<List<int>> MaximalHierarchies
        {
            get { return maximalHierarchies; }
        }

        public List<List<string>> MaximalStringHierarchies
        {
            get { return maximalStringHierarchies; }
        }
    }
}

