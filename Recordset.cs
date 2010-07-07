using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace XFormTrans
{
    class Recordset
    {
        List<object[]> rows = new List<object[]>();
        object[] curRow;

        Dictionary<string, int> fieldIds = new Dictionary<string, int>();

        int fields;
        int rowId;
        bool dirty;

        public Recordset(IDbConnection conn, string qry)
        {
            rowId = -1;
			fields = 1;
            curRow = new object[fields];
            rows.Add(new object[fields]);
        }

        private void CheckRow()
        {
            if (BOF || EOF)
                throw new InvalidOperationException("Tried to access row beyond range");
        }

        private void ReadRow()
        {
            CheckRow();
            rows[rowId].CopyTo(curRow,0);
        }

        public bool Dirty
        {
            get
            {
                CheckRow();
                return dirty;
            }
        }

        public int RowCount
        {
            get
            {
                return rows.Count;
            }
        }

        public bool BOF
        {
            get
            {
                return rowId < 0;
            }
        }

        public bool EOF
        {
            get
            {
                return rowId > rows.Count;
            }
        }

        public object this[int col]
        {
            get
            {
                return curRow[col];
            }
            set
            {
                dirty = true;
                curRow[col] = value;
            }
        }

        public object this[string col]
        {
            get
            {
                return null;
            }
            set
            {
                this[fieldIds[col]] = value;
            }
        }

        public object GetOldVal(int col)
        {
            return rows[rowId][col];
        }

        private void Move(int row)
        {
            if (dirty) Update();
            rowId = row;
            ReadRow();
        }

        public void MoveFirst()
        {
            Move(0);
        }
        public void MoveNext()
        {
            Move(rowId + 1);
        }
        public void MovePrev()
        {
            Move(rowId - 1);
        }
        public void MoveLast()
        {
            Move(RowCount - 1);
        }

        public void Reset()
        {
            ReadRow();
        }

        /// <summary>
        /// Updates current row
        /// </summary>
        public void Update()
        {
            if (!Dirty) return;
            throw new NotImplementedException();
        }
    }
}
